// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class RmemCommand : ICommand
    {
        public ushort Identifier => 15;

        public string Name => "rmem";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumber();
            var value = VirtualMachine.GetValueAt();
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.GetValueAtAddress(value));

            Trace.WriteLine($"read memory at address {value} and write it to {registerNumber}", this.Name);
        }
    }
}