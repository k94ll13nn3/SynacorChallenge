// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class OrCommand : ICommand
    {
        public ushort Identifier => 13;

        public string Name => "or";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var leftValue = VirtualMachine.GetValueAndAdvance();
            var rightValue = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue | rightValue));

            Trace.WriteLine($"stores into {registerNumber} the bitwise or of {leftValue} and {rightValue}", this.Name);
        }
    }
}