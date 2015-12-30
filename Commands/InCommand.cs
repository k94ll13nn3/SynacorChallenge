// SynacorChallenge plugin

using System;
using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class InCommand : ICommand
    {
        public ushort Identifier => 20;

        public string Name => "in";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumber();
            var c = Console.ReadKey();
            VirtualMachine.SetRegisterValue(registerNumber, c.Key == ConsoleKey.Enter ? '\n' : c.KeyChar);

            Trace.WriteLine($"read a character from the terminal and write its ASCII code to {registerNumber}", this.Name);
        }
    }
}