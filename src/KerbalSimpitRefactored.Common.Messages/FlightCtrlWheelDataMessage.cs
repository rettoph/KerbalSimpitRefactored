using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FlightCtrlWheelDataMessage : ISimpitMessageData
    {
        public short Steer { get; set; }
        public short Throttle { get; set; }
        public byte Mask { get; set; }
    }
}
