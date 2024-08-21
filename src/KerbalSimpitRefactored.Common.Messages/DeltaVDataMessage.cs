using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeltaVDataMessage : ISimpitMessageData
    {
        public float StageDeltaV { get; set; }
        public float TotalDeltaV { get; set; }
    }
}
