// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class AddCommand : ICommand
    {
        public int Identifier => 9;

        public string Name => "add";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.Add(leftValue, rightValue));

            Trace.WriteLine($"assign into {registerNumber} the sum of {leftValue} and {rightValue}", this.Name);

            return currentPosition + 4;
        }
    }
}