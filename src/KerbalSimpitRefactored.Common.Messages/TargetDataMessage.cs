using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TargetDataMessage : ISimpitMessageData
    {
        public float Distance { get; set; }
        public float Velocity { get; set; }
        public float Heading { get; set; }
        public float Pitch { get; set; }
        public float VelocityHeading { get; set; }
        public float VelocityPitch { get; set; }
    }
}
