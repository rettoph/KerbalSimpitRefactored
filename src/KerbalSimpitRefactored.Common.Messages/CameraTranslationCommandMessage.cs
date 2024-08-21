using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CameraTranslationCommandMessage : ISimpitMessageData
    {
        public short X;
        public short Y;
        public short Z;
        public byte Mask;
    }
}
