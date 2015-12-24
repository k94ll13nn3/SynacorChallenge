using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class AndCommand : ICommand
    {
        public int Identifier => 12;

        public string Name => "and";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue & rightValue));

            Trace.WriteLine($"stores into {registerNumber} the bitwise and of {leftValue} and {rightValue}", Name);

            return currentPosition + 4;
        }
    }
}