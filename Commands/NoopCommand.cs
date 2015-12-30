// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class NoopCommand : ICommand
    {
        public ushort Identifier => 21;

        public string Name => "noop";

        public void Execute()
        {
            Trace.WriteLine("no operation", this.Name);
        }
    }
}