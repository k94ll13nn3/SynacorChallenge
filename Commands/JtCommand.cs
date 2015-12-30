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
            var value = VirtualMachine.GetValueAt();
            var jump = VirtualMachine.GetValueAt();
            if (value != 0)
            {
                VirtualMachine.SetPosition(jump);
            }

            Trace.WriteLine($"if {value} is nonzero, jump to {jump}", this.Name);
        }
    }
}