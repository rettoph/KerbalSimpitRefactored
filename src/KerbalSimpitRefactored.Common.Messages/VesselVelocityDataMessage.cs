using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VesselVelocityDataMessage : ISimpitMessageData
    {
        public float Orbital { get; set; }
        public float Surface { get; set; }
        public float Vertical { get; set; }
    }
}
