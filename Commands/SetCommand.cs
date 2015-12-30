// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class SetCommand : ICommand
    {
        public ushort Identifier => 1;

        public string Name => "set";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumber();
            var value = VirtualMachine.GetValueAt();
            VirtualMachine.SetRegisterValue(registerNumber, value);

            Trace.WriteLine($"set register {registerNumber} to the value of {value}", this.Name);
        }
    }
}