// SynacorChallenge plugin

using Newtonsoft.Json;

namespace SynacorChallenge
{
    internal class VirtualMachineConfiguration
    {
        public bool LogActivity { get; set; }

        public string LogFileName { get; set; }

        public int Registers { get; set; }

        [JsonIgnore]
        public bool BinaryFileLoaded { get; set; }
    }
}