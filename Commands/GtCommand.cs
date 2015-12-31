// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class GtCommand : ICommand
    {
        public ushort Identifier => 5;

        public string Name => "gt";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var leftValue = VirtualMachine.GetValueAndAdvance();
            var rightValue = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue > rightValue ? 1 : 0));

            Trace.WriteLine($"set {registerNumber} to 1 if {leftValue} is greater than {rightValue}; set it to 0 otherwise", this.Name);
        }
    }
}