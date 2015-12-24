using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class CallCommand : ICommand
    {
        public int Identifier => 17;

        public string Name => "call";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine($"write the address of the next instruction to the stack and jump to {VirtualMachine.GetValueAt(currentPosition + 1)}", Name);
            VirtualMachine.PushToStack((ushort)(currentPosition + 2));

            return VirtualMachine.GetValueAt(currentPosition + 1);
        }
    }
}