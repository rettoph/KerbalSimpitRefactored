using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FlightCtrlTranslationDataMessage : ISimpitMessageData
    {
        public short X { get; set; }
        public short Y { get; set; }
        public short Z { get; set; }
        public byte Mask { get; set; }
    }
}
