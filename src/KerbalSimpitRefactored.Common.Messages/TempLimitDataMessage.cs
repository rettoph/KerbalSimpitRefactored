using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TempLimitDataMessage : ISimpitMessageData
    {
        public byte TempLimitPercentage { get; set; }
        public byte SkinTempLimitPercentage { get; set; }
    }
}
