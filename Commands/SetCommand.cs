// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class SetCommand : ICommand
    {
        public int Identifier => 1;

        public string Name => "set";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            var value = VirtualMachine.GetValueAt(currentPosition + 2);
            VirtualMachine.SetRegisterValue(registerNumber, value);

            Trace.WriteLine($"set register {registerNumber} to the value of {value}", this.Name);
            return currentPosition + 3;
        }
    }
}