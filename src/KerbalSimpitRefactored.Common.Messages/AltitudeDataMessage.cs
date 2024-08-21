using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AltitudeDataMessage : ISimpitMessageData
    {
        public float Alt { get; set; }
        public float SurfAlt { get; set; }
    }
}
