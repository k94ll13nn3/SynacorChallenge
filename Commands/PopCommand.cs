// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class PopCommand : ICommand
    {
        public ushort Identifier => 3;

        public string Name => "pop";

        public void Execute()
        {
            var value = VirtualMachine.PopFromStack();
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, value);

            Trace.WriteLine($"remove the top element from the stack and write it into {registerNumber}", this.Name);
        }
    }
}