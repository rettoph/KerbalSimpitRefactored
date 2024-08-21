using KerbalSimpitRefactored.Common.Messages.Enums;
using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WarpChangeCommandMessage : ISimpitMessageData
    {
        public WarpChangeRateEnum Rate { get; set; }
    }
}
