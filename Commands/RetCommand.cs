// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class RetCommand : ICommand
    {
        public ushort Identifier => 18;

        public string Name => "ret";

        public void Execute()
        {
            VirtualMachine.SetPosition(VirtualMachine.PopFromStack());

            Trace.WriteLine("remove the top element from the stack and jump to it", this.Name);
        }
    }
}