using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeltaVEnvDataMessage : ISimpitMessageData
    {
        public float StageDeltaVASL { get; set; }
        public float TotalDeltaVASL { get; set; }
        public float StageDeltaVVac { get; set; }
        public float TotalDeltaVVac { get; set; }
    }
}
