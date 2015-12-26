// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class JtCommand : ICommand
    {
        public int Identifier => 7;

        public string Name => "jt";

        public uint Execute(uint currentPosition)
        {
            var value = VirtualMachine.GetValueAt(currentPosition + 2);

            Trace.WriteLine($"if {VirtualMachine.GetValueAt(currentPosition + 1)} is nonzero, jump to {value}", this.Name);
            if (VirtualMachine.GetValueAt(currentPosition + 1) != 0)
            {
                return value;
            }

            return currentPosition + 3;
        }
    }
}