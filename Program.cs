using System;
using System.Diagnostics;
using System.IO;

namespace SynacorChallenge
{
    internal class Program
    {
        private static void Main()
        {
            File.Delete("virtual-machine.log");
            using (var textWriterTraceListener = new TextWriterTraceListener("virtual-machine.log"))
            {
                Trace.Listeners.Add(textWriterTraceListener);
            }
            Trace.AutoFlush = true;

            VirtualMachine.LoadFile(@"..\..\challenge.bin");
            VirtualMachine.Run();

            Console.WriteLine("\nProcess terminated");
            Console.ReadKey();
        }
    }
}