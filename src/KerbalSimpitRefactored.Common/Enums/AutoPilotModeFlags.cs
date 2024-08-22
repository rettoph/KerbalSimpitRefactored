using System;

namespace KerbalSimpitRefactored.Common.Enums
{
    [Flags]
    public enum AutoPilotModeFlags : ushort
    {
        None = 0,
        StabilityAssist = 1,
        Prograde = 2,
        Retrograde = 4,
        Normal = 8,
        Antinormal = 16,
        RadialIn = 32,
        RadialOut = 64,
        Target = 128,
        AntiTarget = 256,
        Maneuver = 512
    }
}
