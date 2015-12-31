// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class AndCommand : ICommand
    {
        public ushort Identifier => 12;

        public string Name => "and";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var leftValue = VirtualMachine.GetValueAndAdvance();
            var rightValue = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue & rightValue));

            Trace.WriteLine($"stores into {registerNumber} the bitwise and of {leftValue} and {rightValue}", this.Name);
        }
    }
}