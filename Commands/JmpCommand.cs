// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class JmpCommand : ICommand
    {
        public ushort Identifier => 6;

        public string Name => "jmp";

        public void Execute()
        {
            var value = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetPosition(value);

            Trace.WriteLine($"jump to {value}", this.Name);
        }
    }
}