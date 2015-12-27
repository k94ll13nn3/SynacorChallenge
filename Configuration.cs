// SynacorChallenge plugin

using Newtonsoft.Json;

namespace SynacorChallenge
{
    // http://graphics.stanford.edu/~seander/bithacks.html
    internal class Configuration
    {
        private static readonly int[] PowerOfTwo =
            { 0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 };

        public int ArchitectureBits
        {
            get
            {
                return PowerOfTwo[(this.IntegerLimit * 0x077CB531) >> 27];
            }

            set
            {
                this.IntegerLimit = (uint)(1 << value);
            }
        }

        [JsonIgnore]
        public bool BinaryFileLoaded { get; set; }

        [JsonIgnore]
        public uint IntegerLimit { get; set; }

        public bool LogActivity { get; set; }

        public string LogFileName { get; set; }

        public int Registers { get; set; }
    }
}