using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FlightCtrlRotationDataMessage : ISimpitMessageData
    {
        public short Pitch { get; set; }
        public short Roll { get; set; }
        public short Yaw { get; set; }
        public byte Mask { get; set; }
    }
}
