// SynacorChallenge plugin

using System.Linq;

namespace SynacorChallenge
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            VirtualMachine.Initialize("vm-config.json");

            if (args[0] == "run" && args.Count() > 1)
            {
                VirtualMachine.LoadFile(args[1]);
                VirtualMachine.Run();
            }
            else if (args[0] == "compile" && args.Count() > 2)
            {
                VirtualMachine.Compile(args[1], args[2]);
            }
        }
    }
}