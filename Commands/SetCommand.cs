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
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var value = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, value);

            Trace.WriteLine($"set register {registerNumber} to the value of {value}", this.Name);
        }
    }
}