using System;

namespace KerbalSimpitRefactored.Common.Enums
{
    [Flags]
    public enum FlightStatusFlags : byte
    {
        IsInFlight = 1,
        IsEva = 2,
        IsRecoverable = 4,
        IsInAtmoTW = 8,
        ComnetControlLevel0 = 16,
        ComnetControlLevel1 = 32,
        HasTargetSet = 64
    }
}
