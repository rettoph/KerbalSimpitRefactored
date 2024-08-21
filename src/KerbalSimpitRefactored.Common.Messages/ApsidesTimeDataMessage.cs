using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ApsidesTimeDataMessage : ISimpitMessageData
    {
        public int Periapsis { get; set; }
        public int Apoapsis { get; set; }
    }
}
