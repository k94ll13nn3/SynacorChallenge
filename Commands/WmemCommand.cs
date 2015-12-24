using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynacorChallenge.Commands
{
    internal class WmemCommand : ICommand
    {
        public int Identifier => 16;

        public string Name => "wmem";

        public uint Execute(uint currentPosition)
        {
            var address = VirtualMachine.GetValueAt(currentPosition + 1);
            var value = VirtualMachine.GetValueAt(currentPosition + 2);

            Trace.WriteLine($"write the value from {currentPosition + 2} into memory at address {address}", Name);

            VirtualMachine.WriteMemory(address, value);

            return currentPosition + 3;
        }
    }
}