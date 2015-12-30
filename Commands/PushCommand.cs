// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class PushCommand : ICommand
    {
        public ushort Identifier => 2;

        public string Name => "push";

        public void Execute()
        {
            var value = VirtualMachine.GetValueAt();
            VirtualMachine.PushToStack(value);

            Trace.WriteLine($"push {value} onto the stack", this.Name);
        }
    }
}