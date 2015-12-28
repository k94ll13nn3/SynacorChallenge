// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class RmemCommand : ICommand
    {
        public int Identifier => 15;

        public string Name => "rmem";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var value = VirtualMachine.GetValueAt(currentPosition + 2);

            Trace.WriteLine($"read memory at address {value} and write it to {registerNumber}", this.Name);

            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.GetValueAt(value));

            return currentPosition + 3;
        }
    }
}