using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CustomAxisCommandMessage : ISimpitMessageData
    {
        public short Custom1 { get; set; }
        public short Custom2 { get; set; }
        public short Custom3 { get; set; }
        public short Custom4 { get; set; }
        public byte Mask { get; set; }
    }
}
