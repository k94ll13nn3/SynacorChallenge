// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class RetCommand : ICommand
    {
        public int Identifier => 18;

        public string Name => "ret";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine("remove the top element from the stack and jump to it", this.Name);
            return VirtualMachine.PopFromStack();
        }
    }
}