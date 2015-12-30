// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class HaltCommand : ICommand
    {
        public ushort Identifier => 0;

        public string Name => "halt";

        public void Execute()
        {
            VirtualMachine.Terminate();

            Trace.WriteLine("stop execution and terminate the program", this.Name);
        }
    }
}