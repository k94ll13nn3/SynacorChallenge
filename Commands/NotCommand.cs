// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class NotCommand : ICommand
    {
        public int Identifier => 14;

        public string Name => "not";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var value = VirtualMachine.GetValueAt(currentPosition + 2);
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.NegateValue(value));

            Trace.WriteLine($"stores bitwise inverse of {value} in {registerNumber}", this.Name);

            return currentPosition + 3;
        }
    }
}