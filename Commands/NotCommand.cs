﻿// SynacorChallenge plugin

using System.Diagnostics;

namespace SynacorChallenge.Commands
{
    internal class NotCommand : ICommand
    {
        public ushort Identifier => 14;

        public string Name => "not";

        public void Execute()
        {
            var registerNumber = VirtualMachine.GetRegisterNumberAndAdvance();
            var value = VirtualMachine.GetValueAndAdvance();
            VirtualMachine.SetRegisterValue(registerNumber, VirtualMachine.NegateValue(value));

            Trace.WriteLine($"stores bitwise inverse of {value} in {registerNumber}", this.Name);
        }
    }
}