using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynacorChallenge.Commands
{
    internal class InCommand : ICommand
    {
        public int Identifier => 20;

        public string Name => "in";

        public uint Execute(uint currentPosition)
        {
            var registerNumber = VirtualMachine.GetRegisterNumber(currentPosition + 1);
            Trace.WriteLine($"read a character from the terminal and write its ASCII code to {registerNumber}", Name);

            var c = Console.ReadKey().KeyChar;

            if (c == '\r')
            {
                c = '\n';
            }

            VirtualMachine.SetRegisterValue(registerNumber, c);

            return currentPosition + 2;
        }
    }
}