using KerbalSimpitRefactored.Common.Messages.Enums;
using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct FlightStatusDataMessage : ISimpitMessageData
    {
        public FlightStatusFlags Status;
        public byte VesselSituation; // See Vessel.Situations for possible values
        public byte CurrentTWIndex;
        public byte CrewCapacity;
        public byte CrewCount;
        public byte CommNetSignalStrenghPercentage;
        public byte CurrentStage;
        public byte VesselType;
    }
}
