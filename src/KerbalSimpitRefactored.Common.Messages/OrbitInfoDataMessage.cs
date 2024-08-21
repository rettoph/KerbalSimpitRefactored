using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OrbitInfoDataMessage : ISimpitMessageData
    {
        public float Eccentricity { get; set; }
        public float SemiMajorAxis { get; set; }
        public float Inclination { get; set; }
        public float LongAscendingNode { get; set; }
        public float ArgPeriapsis { get; set; }
        public float TrueAnomaly { get; set; }
        public float MeanAnomaly { get; set; }
        public float Period { get; set; }
    }
}
