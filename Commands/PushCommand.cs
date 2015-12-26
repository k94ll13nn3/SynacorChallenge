// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class PushCommand : ICommand
    {
        public int Identifier => 2;

        public string Name => "push";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine($"push {VirtualMachine.GetValueAt(currentPosition + 1)} onto the stack", this.Name);
            VirtualMachine.PushToStack(VirtualMachine.GetValueAt(currentPosition + 1));

            return currentPosition + 2;
        }
    }
}