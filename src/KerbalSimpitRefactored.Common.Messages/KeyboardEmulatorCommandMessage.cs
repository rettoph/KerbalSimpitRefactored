using KerbalSimpitRefactored.Common.Messages.Enums;
using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Core.KSP.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct KeyboardEmulatorCommandMessage : ISimpitMessageData
    {
        public KeyboardEmulatorModifierFlags Modifier;
        public short Key;
    }
}
