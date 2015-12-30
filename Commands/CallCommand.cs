// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class CallCommand : ICommand
    {
        public ushort Identifier => 17;

        public string Name => "call";

        public void Execute()
        {
            var value = VirtualMachine.GetValueAt();
            VirtualMachine.PushToStack(VirtualMachine.GetPosition());
            VirtualMachine.SetPosition(value);

            Trace.WriteLine($"write the address of the next instruction to the stack and jump to {value}", this.Name);
        }
    }
}