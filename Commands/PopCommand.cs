// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class PopCommand : ICommand
    {
        public int Identifier => 3;

        public string Name => "pop";

        public uint Execute(uint currentPosition)
        {
            var value = VirtualMachine.PopFromStack();
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            VirtualMachine.SetRegisterValue(registerNumber, value);
            Trace.WriteLine($"remove the top element from the stack and write it into {registerNumber}", this.Name);

            return currentPosition + 2;
        }
    }
}