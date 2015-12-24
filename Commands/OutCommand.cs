using System;
using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class OutCommand : ICommand
    {
        public int Identifier => 19;

        public string Name => "out";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine($"write the character represented by ASCII code {VirtualMachine.GetValueAt(currentPosition + 1)} to the terminal", Name);
            Console.Write((char)VirtualMachine.GetValueAt(currentPosition + 1));

            return currentPosition + 2;
        }
    }
}