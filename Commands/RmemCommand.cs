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
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var value = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.GetValueAtAddress(value));

            Trace.WriteLine($"read memory at address {value} and write it to {registerNumber}", this.Name);
        }
    }
}