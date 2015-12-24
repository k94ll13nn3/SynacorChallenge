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
        private const uint numberOfRegisters = 8;
        private const uint registersStart = 32768;
        private static readonly ushort[] registers = new ushort[numberOfRegisters];
        private static readonly Stack<ushort> stack = new Stack<ushort>();
        private static ushort[] content;

        // Adds two value
        public static ushort Add(ushort leftValue, ushort rightValue)
        {
            return (ushort)((leftValue + rightValue) % registersStart);
        }

        internal static ushort Mult(ushort leftValue, ushort rightValue)
        {
            return (ushort)((leftValue * rightValue) % registersStart);
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

            return value - registersStart;
        }

        // Returns the value corresponding to the position specified.
        public static ushort GetValueAt(uint position)
        {
            var value = content[position];

            if (IsRegister(value))
            {
                return GetRegisterValue(value - registersStart);
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
            }
        }

        public static ushort PopFromStack()
        {
            return stack.Pop();
        }

        public static void PushToStack(ushort value)
        {
            stack.Push(value);
        }

        public static void Run()
        {
            var pos = ProcessCommand(0);

            while (pos != int.MaxValue)
            {
                pos = ProcessCommand(pos);
            }
        }

        // Set the registerNumber-th register to value.
        public static void SetRegisterValue(uint registerNumber, ushort value)
        {
            if (registerNumber >= numberOfRegisters)
            {
                throw new ArgumentException("Invalid register number.", nameof(registerNumber));
            }

            registers[registerNumber] = value;
        }

        // Returns the value of the registerNumber-th register.
        private static ushort GetRegisterValue(uint registerNumber)
        {
            if (registerNumber >= numberOfRegisters)
            {
                throw new ArgumentException("Invalid register number.", nameof(registerNumber));
            }

            return registers[registerNumber];
        }

        // Test if the value describe a register.
        private static bool IsRegister(uint value)
        {
            return value >= registersStart && value <= (registersStart + numberOfRegisters);
        }

        public static ushort NegateValue(ushort value)
        {
            return (ushort)(~value & 0x7FFF);
        }

        public static void WriteMemory(ushort address, ushort value)
        {
            content[address] = value;
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