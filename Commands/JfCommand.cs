// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class JfCommand : ICommand
    {
        public int Identifier => 8;

        public string Name => "jf";

        public uint Execute(uint currentPosition)
        {
            var value = VirtualMachine.GetValueAt(currentPosition + 2);

            Trace.WriteLine($"if {VirtualMachine.GetValueAt(currentPosition + 1)} is zero, jump to {value}", this.Name);
            if (VirtualMachine.GetValueAt(currentPosition + 1) == 0)
            {
                return value;
            }

            return currentPosition + 3;
        }
    }
}