using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class NoopCommand : ICommand
    {
        public int Identifier => 21;

        public string Name => "noop";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine("no operation", Name);
            return currentPosition + 1;
        }
    }
}