// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class ModCommand : ICommand
    {
        public ushort Identifier => 11;

        public string Name => "mod";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumber();
            var leftValue = VirtualMachine.GetValueAt();
            var rightValue = VirtualMachine.GetValueAt();
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue % rightValue));

            Trace.WriteLine($"store into {registerNumber} the remainder of {leftValue} divided by {rightValue}", this.Name);
        }
    }
}