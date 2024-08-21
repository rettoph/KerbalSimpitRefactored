using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CameraRotationCommandMessage : ISimpitMessageData
    {
        public short Pitch;
        public short Roll;
        public short Yaw;
        public short Zoom;
        public byte Mask;
    }
}
