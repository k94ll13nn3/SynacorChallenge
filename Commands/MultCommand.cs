// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class MultCommand : ICommand
    {
        public ushort Identifier => 10;

        public string Name => "mult";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumber();
            var leftValue = VirtualMachine.GetValueAt();
            var rightValue = VirtualMachine.GetValueAt();
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.Mult(leftValue, rightValue));

            Trace.WriteLine($"store into {registerNumber} the product of {leftValue} and {rightValue}", this.Name);
        }
    }
}