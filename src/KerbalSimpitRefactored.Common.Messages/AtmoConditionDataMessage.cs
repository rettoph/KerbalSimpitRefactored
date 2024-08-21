using KerbalSimpitRefactored.Common.Messages.Enums;
using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AtmoConditionDataMessage : ISimpitMessageData
    {
        public AtmoCharacteristicsFlags AtmoCharacteristics;
        public float AirDensity;
        public float Temperature;
        public float Pressure;
    }
}
