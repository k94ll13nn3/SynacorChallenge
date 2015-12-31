// SynacorChallenge plugin

using System;
using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class OutCommand : ICommand
    {
        public ushort Identifier => 19;

        public string Name => "out";

        public void Execute()
        {
            var value = VirtualMachine.GetValueAndAdvance();
            Console.Write((char)value);

            Trace.WriteLine($"write the character represented by ASCII code {value} to the terminal", this.Name);
        }
    }
}