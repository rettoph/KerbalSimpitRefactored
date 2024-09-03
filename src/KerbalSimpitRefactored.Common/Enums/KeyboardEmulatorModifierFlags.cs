using System;

namespace KerbalSimpitRefactored.Common.Enums
{
    [Flags]
    public enum KeyboardEmulatorModifierFlags : byte
    {
        SHIFT_MOD = 1,
        CTRL_MOD = 2,
        ALT_MOD = 4,
        KEY_DOWN_MOD = 8,
        KEY_UP_MOD = 16
    }
}
