using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class JmpCommand : ICommand
    {
        public int Identifier => 6;

        public string Name => "jmp";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine($"jump to {VirtualMachine.GetValueAt(currentPosition + 1)}", Name);
            return VirtualMachine.GetValueAt(currentPosition + 1);
        }
    }
}