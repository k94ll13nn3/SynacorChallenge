// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class AddCommand : ICommand
    {
        public ushort Identifier => 9;

        public string Name => "add";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var leftValue = VirtualMachine.GetValueAndAdvance();
            var rightValue = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.Add(leftValue, rightValue));

            Trace.WriteLine($"assign into {registerNumber} the sum of {leftValue} and {rightValue}", this.Name);
        }
    }
}