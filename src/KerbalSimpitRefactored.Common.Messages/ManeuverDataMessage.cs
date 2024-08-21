using SimpitRefactored.Common.Core;

namespace KerbalSimpitRefactored.Common.Messages
{
    public struct ManeuverDataMessage : ISimpitMessageData
    {
        public float TimeToNextManeuver { get; set; }
        public float DeltaVNextManeuver { get; set; }
        public float DurationNextManeuver { get; set; }
        public float DeltaVTotal { get; set; }
        public float HeadingNextManeuver { get; set; }
        public float PitchNextManeuver { get; set; }
    }
}
