using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AirSpeedDataMessage : ISimpitMessageData
    {
        public float IndicatedAirSpeed { get; set; }
        public float MachNumber { get; set; }
        public float GeeForce { get; set; }
    }
}
