// SynacorChallenge plugin

using Newtonsoft.Json;
using SynacorChallenge.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SynacorChallenge
{
    internal static class VirtualMachine
    {
        private const uint NumberOfRegisters = 8;
        private const uint RegistersStart = 32768;
        private static readonly ushort[] Registers = new ushort[NumberOfRegisters];
        private static readonly Stack<ushort> Stack = new Stack<ushort>();

        private static ushort[] content;

        private static VirtualMachineConfiguration config;

        // Adds two value
        public static ushort Add(ushort leftValue, ushort rightValue)
        {
            return (ushort)((leftValue + rightValue) % RegistersStart);
        }

        // Return the number of the register designated by value,
        // i.e. a value from 0 to numberOfRegisters.
        public static uint GetRegisterNumber(uint position)
        {
            var value = content[position];
            if (!IsRegister(value))
            {
                throw new ArgumentException("The position does not link to a register.", nameof(position));
            }

            return value - RegistersStart;
        }

        // Returns the value corresponding to the position specified.
        public static ushort GetValueAt(uint position)
        {
            var value = content[position];

            if (IsRegister(value))
            {
                return GetRegisterValue(value - RegistersStart);
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
            return (ushort)(~value & 0x7FFF);
        }

        public static ushort PopFromStack()
        {
            return Stack.Pop();
        }

        public static void PushToStack(ushort value)
        {
            Stack.Push(value);
        }

        public static void Run()
        {
            if (config.BinaryFileLoaded)
            {
                var pos = ProcessCommand(0);

                while (pos != int.MaxValue)
                {
                    pos = ProcessCommand(pos);
                }
            }
        }

        // Set the registerNumber-th register to value.
        public static void SetRegisterValue(uint registerNumber, ushort value)
        {
            if (registerNumber >= NumberOfRegisters)
            {
                throw new ArgumentException("Invalid register number.", nameof(registerNumber));
            }

            Registers[registerNumber] = value;
        }

        public static void WriteMemory(ushort address, ushort value)
        {
            content[address] = value;
        }

        internal static void Initialize(string configFile)
        {
            config = JsonConvert.DeserializeObject<VirtualMachineConfiguration>(File.ReadAllText(configFile));

            if (config.LogActivity)
            {
                Console.WriteLine($"Trace will be written to {config.LogFileName}");

                Trace.AutoFlush = true;
                File.Delete(config.LogFileName);
                using (var textWriterTraceListener = new TextWriterTraceListener(config.LogFileName))
                {
                    Trace.Listeners.Add(textWriterTraceListener);
                }
            }
        }

        internal static ushort Mult(ushort leftValue, ushort rightValue)
        {
            return (ushort)((leftValue * rightValue) % RegistersStart);
        }

        // Returns the value of the registerNumber-th register.
        private static ushort GetRegisterValue(uint registerNumber)
        {
            if (registerNumber >= NumberOfRegisters)
            {
                throw new ArgumentException("Invalid register number.", nameof(registerNumber));
            }

            return Registers[registerNumber];
        }

        // Test if the value describe a register.
        private static bool IsRegister(uint value)
        {
            return value >= RegistersStart && value <= (RegistersStart + NumberOfRegisters);
        }

        private static uint ProcessCommand(uint currentPosition)
        {
            var command = content[currentPosition];

            var commandList = new List<ICommand>
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

            var commandToExecute = commandList.FirstOrDefault(c => c.Identifier == command);

            if (commandToExecute != null)
            {
                return commandToExecute.Execute(currentPosition);
            }

            Trace.WriteLine($"Unknown command : {command}");

            return currentPosition + 1;
        }
    }
}