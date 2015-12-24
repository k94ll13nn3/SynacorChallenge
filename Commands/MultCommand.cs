using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class MultCommand : ICommand
    {
        public int Identifier => 10;

        public string Name => "mult";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.Mult(leftValue, rightValue));

            Trace.WriteLine($"store into {registerNumber} the product of {leftValue} and {rightValue}", Name);

            return currentPosition + 4;
        }
    }
}