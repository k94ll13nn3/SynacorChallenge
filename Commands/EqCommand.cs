using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class EqCommand : ICommand
    {
        public int Identifier => 4;

        public string Name => "eq";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);

            Trace.WriteLine($"set {registerNumber} to 1 if {leftValue} is equal to {rightValue}; set it to 0 otherwise", Name);

            if (leftValue == rightValue)
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