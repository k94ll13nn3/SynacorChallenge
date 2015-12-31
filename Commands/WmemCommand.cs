// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class WmemCommand : ICommand
    {
        public ushort Identifier => 16;

        public string Name => "wmem";

        public void Execute()
        {
            var address = VirtualMachine.GetValueAndAdvance();
            var value = VirtualMachine.GetValueAndAdvance();

            Trace.WriteLine($"write {value} into memory at address {address}", this.Name);

            VirtualMachine.WriteMemory(address, value);
        }
    }
}