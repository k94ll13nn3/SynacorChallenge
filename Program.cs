// SynacorChallenge plugin

using System;

namespace SynacorChallenge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            VirtualMachine.Initialize("vm-config.json");
            VirtualMachine.LoadFile(args[0]);
            VirtualMachine.Run();

            Console.WriteLine("\nProcess terminated");
            Console.ReadKey();
        }
    }
}