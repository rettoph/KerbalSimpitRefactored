using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct VesselRotationDataMessage : ISimpitMessageData
    {
        public float Heading { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float OrbitalVelocityHeading { get; set; }
        public float OrbitalVelocityPitch { get; set; }
        public float SurfaceVelocityHeading { get; set; }
        public float SurfaceVelocityPitch { get; set; }
    }
}
