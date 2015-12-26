// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class ModCommand : ICommand
    {
        public int Identifier => 11;

        public string Name => "mod";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var leftValue = VirtualMachine.GetValueAt(currentPosition + 2);
            var rightValue = VirtualMachine.GetValueAt(currentPosition + 3);
            VirtualMachine.SetRegisterValue(registerNumber, (ushort)(leftValue % rightValue));

            Trace.WriteLine($"store into {registerNumber} the remainder of {leftValue} divided by {rightValue}", this.Name);

            return currentPosition + 4;
        }
    }
}