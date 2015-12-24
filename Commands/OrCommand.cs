using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class OrCommand : ICommand
    {
        public int Identifier => 13;

        public string Name => "or";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue | rightValue));

            Trace.WriteLine($"stores into {registerNumber} the bitwise or of {leftValue} and {rightValue}", Name);

            return currentPosition + 4;
        }
    }
}