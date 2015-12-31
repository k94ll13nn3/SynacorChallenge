// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class JtCommand : ICommand
    {
        public ushort Identifier => 7;

        public string Name => "jt";

        public void Execute()
        {
            var value = VirtualMachine.GetValueAndAdvance();
            var jump = VirtualMachine.GetValueAndAdvance();
            if (value != 0)
            {
                VirtualMachine.SetPosition(jump);
            }

            Trace.WriteLine($"if {value} is nonzero, jump to {jump}", this.Name);
        }
    }
}