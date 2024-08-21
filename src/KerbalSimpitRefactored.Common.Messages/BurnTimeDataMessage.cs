using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BurnTimeDataMessage : ISimpitMessageData
    {
        public float StageBurnTime { get; set; }
        public float TotalBurnTime { get; set; }
    }
}
