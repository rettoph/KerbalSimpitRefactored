using System;

namespace KerbalSimpitRefactored.Common.Enums
{
    [Flags]
    public enum AtmoCharacteristicsFlags : byte
    {
        None = 0,
        HasAtmosphere = 1,
        HasOxygen = 2,
        IsVesselInAtmosphere = 4
    }
}
