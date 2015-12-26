// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class GtCommand : ICommand
    {
        public int Identifier => 5;

        public string Name => "gt";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);

            Trace.WriteLine($"set {registerNumber} to 1 if {leftValue} is greater than {rightValue}; set it to 0 otherwise", this.Name);

            if (leftValue > rightValue)
            {
                VirtualMachine.SetRegisterValue(registerNumber, 1);
            }
            else
            {
                VirtualMachine.SetRegisterValue(registerNumber, 0);
            }

            return currentPosition + 4;
        }
    }
}