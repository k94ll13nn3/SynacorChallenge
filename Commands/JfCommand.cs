// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class JfCommand : ICommand
    {
        public ushort Identifier => 8;

        public string Name => "jf";

        public void Execute()
        {
            var value = VirtualMachine.GetValueAt();
            var jump = VirtualMachine.GetValueAt();
            if (value == 0)
            {
                VirtualMachine.SetPosition(jump);
            }

            Trace.WriteLine($"if {value} is zero, jump to {jump}", this.Name);
        }
    }
}