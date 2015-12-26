// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class HaltCommand : ICommand
    {
        public int Identifier => 0;

        public string Name => "halt";

        public uint Execute(uint currentPosition)
        {
            Trace.WriteLine("stop execution and terminate the program", this.Name);
            return int.MaxValue;
        }
    }
}