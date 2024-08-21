using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ApsidesDataMessage : ISimpitMessageData
    {
        public float Periapsis { get; set; }
        public float Apoapsis { get; set; }
    }
}
