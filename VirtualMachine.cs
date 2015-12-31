// SynacorChallenge plugin

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SynacorChallenge.Commands;

namespace SynacorChallenge
{
    internal static class VirtualMachine
    {
        private static readonly Stack<ushort> Stack = new Stack<ushort>();
        private static List<ICommand> commandList;
        private static Configuration config;
        private static ushort[] content;

        private static int currentPosition;
        private static bool processTerminated;

        private static ushort[] registers;

        // Adds two value
        public static ushort Add(ushort leftValue, ushort rightValue)
        {
            var value = leftValue + rightValue;
            return (ushort)(value % config.IntegerLimit);
        }

        public static uint GetRegisterNumberAndAdvance()
        {
            var value = content[currentPosition];
            currentPosition++;
            if (!IsRegister(value))
            {
                throw new ArgumentException("The position does not link to a register.");
            }

            return value - config.IntegerLimit;
        }

        public static ushort GetValueAndAdvance()
        {
            var value = content[currentPosition];
            currentPosition++;

            if (IsRegister(value))
            {
                return GetRegisterValue(value - config.IntegerLimit);
            }

            return value;
        }

        public static void LoadFile(string fileName)
        {
            using (BinaryReader b = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                var i = 0;
                var length = b.BaseStream.Length;
                content = new ushort[length / 2];
                while (i * 2 < length)
                {
                    content[i] = b.ReadUInt16();
                    i++;
                }

                config.BinaryFileLoaded = true;
            }
        }

        public static ushort NegateValue(ushort value)
        {
            return (ushort)(~value & (config.IntegerLimit - 1));
        }

        public static ushort PopFromStack()
        {
            var poppedValue = Stack.Pop();
            return poppedValue;
        }

        public static void PushToStack(ushort value)
        {
            Stack.Push(value);
        }

        public static void Run()
        {
            if (config.BinaryFileLoaded)
            {
                while (!processTerminated)
                {
                    ProcessCommand();
                }
            }
        }

        public static void SetPosition(int position)
        {
            currentPosition = position;
        }

        // Set the registerNumber-th register to value.
        public static void SetRegisterValue(uint registerNumber, ushort value)
        {
            if (registerNumber >= config.Registers)
            {
                throw new ArgumentException("Invalid register number.", nameof(registerNumber));
            }

            registers[registerNumber] = value;
        }

        public static void Terminate()
        {
            processTerminated = true;
        }

        public static void WriteMemory(ushort address, ushort value)
        {
            content[address] = value;
        }

        internal static void Compile(string inFile, string outFile)
        {
            var lines = File.ReadLines(inFile);

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    foreach (var line in lines)
                    {
                        var tokens = line.Split(' ');

                        try
                        {
                            writer.Write(commandList.Single(c => c.Name == tokens[0]).Identifier);
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine($"Unknown command : {tokens[0]}");
                            Console.WriteLine("Compilation aborted");
                            return;
                        }

                        for (int i = 1; i < tokens.Length; i++)
                        {
                            writer.Write(ushort.Parse(tokens[i]));
                        }
                    }

                    using (FileStream file = new FileStream(outFile, FileMode.CreateNew))
                    {
                        stream.WriteTo(file);
                    }
                }
            }
        }

        internal static void Decompile(string inFile, string outFile)
        {
            ushort[] binContent;
            using (BinaryReader b = new BinaryReader(File.Open(inFile, FileMode.Open)))
            {
                var i = 0;
                var length = b.BaseStream.Length;
                binContent = new ushort[length / 2];
                while (i * 2 < length)
                {
                    binContent[i] = b.ReadUInt16();
                    i++;
                }
            }

            var noCommands = new int[] { 0, 18, 21 };
            var oneCommands = new int[] { 2, 3, 6, 17, 19, 20 };
            var twoCommands = new int[] { 1, 7, 8, 14, 15, 16 };
            var threeCommands = new int[] { 4, 5, 9, 10, 11, 12, 13 };

            var lines = new List<string>();
            for (int cursor = 0; cursor < binContent.Length; cursor++)
            {
                var command = commandList.Single(c => c.Identifier == binContent[cursor]);
                if (noCommands.Contains(command.Identifier))
                {
                    lines.Add(command.Name);
                }
                else if (oneCommands.Contains(command.Identifier))
                {
                    lines.Add($"{command.Name} {binContent[cursor + 1]}");
                    cursor += 1;
                }
                else if (twoCommands.Contains(command.Identifier))
                {
                    lines.Add($"{command.Name} {binContent[cursor + 1]} {binContent[cursor + 2]}");
                    cursor += 2;
                }
                else if (threeCommands.Contains(command.Identifier))
                {
                    lines.Add($"{command.Name} {binContent[cursor + 1]} {binContent[cursor + 2]} {binContent[cursor + 3]}");
                    cursor += 3;
                }
            }

            File.WriteAllLines(outFile, lines);
        }

        internal static ushort GetPosition()
        {
            return (ushort)currentPosition;
        }

        internal static ushort GetValueAtAddress(ushort position)
        {
            var value = content[position];

            if (IsRegister(value))
            {
                return GetRegisterValue(value - config.IntegerLimit);
            }

            return value;
        }

        internal static void Initialize(string configFile)
        {
            config = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(configFile));

            registers = new ushort[config.Registers];

            if (config.LogActivity)
            {
                Trace.AutoFlush = true;
                File.Delete(config.LogFileName);
                using (var textWriterTraceListener = new TextWriterTraceListener(config.LogFileName))
                {
                    Trace.Listeners.Add(textWriterTraceListener);
                }
            }

            commandList = new List<ICommand>
            {
                new OutCommand(),
                new HaltCommand(),
                new JmpCommand(),
                new NoopCommand(),
                new SetCommand(),
                new JtCommand(),
                new JfCommand(),
                new AddCommand(),
                new EqCommand(),
                new PushCommand(),
                new PopCommand(),
                new GtCommand(),
                new AndCommand(),
                new OrCommand(),
                new NotCommand(),
                new CallCommand(),
                new MultCommand(),
                new ModCommand(),
                new RmemCommand(),
                new WmemCommand(),
                new RetCommand(),
                new InCommand()
            };
        }

        internal static ushort Mult(ushort leftValue, ushort rightValue)
        {
            var value = leftValue * rightValue;
            return (ushort)(value % config.IntegerLimit);
        }

        // Returns the value of the registerNumber-th register.
        private static ushort GetRegisterValue(uint registerNumber)
        {
            if (registerNumber >= config.Registers)
            {
                throw new ArgumentException("Invalid register number.", nameof(registerNumber));
            }

            return registers[registerNumber];
        }

        // Test if the value describe a register.
        private static bool IsRegister(uint value)
        {
            return value >= config.IntegerLimit && value <= (config.IntegerLimit + config.Registers);
        }

        private static void ProcessCommand()
        {
            var command = content[currentPosition];
            currentPosition++;

            var commandToExecute = commandList.FirstOrDefault(c => c.Identifier == command);

            if (commandToExecute != null)
            {
                commandToExecute.Execute();
            }
            else
            {
                Trace.WriteLine($"Unknown command : {command}");
            }
        }
    }
}