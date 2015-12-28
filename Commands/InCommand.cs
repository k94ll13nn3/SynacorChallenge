// SynacorChallenge plugin

using System;
using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class InCommand : ICommand
    {
        public int Identifier => 20;

        public string Name => "in";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            Trace.WriteLine($"read a character from the terminal and write its ASCII code to {registerNumber}", this.Name);

            var c = Console.ReadKey();

            if (c.Key == ConsoleKey.Enter)
            {
                VirtualMachine.SetRegisterValue(registerNumber, '\n');
            }
            else
            {
                VirtualMachine.SetRegisterValue(registerNumber, c.KeyChar);
            }

            return currentPosition + 2;
        }
    }
}