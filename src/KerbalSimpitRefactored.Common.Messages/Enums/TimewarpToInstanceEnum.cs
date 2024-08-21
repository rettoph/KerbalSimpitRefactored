namespace KerbalSimpitRefactored.Common.Messages.Enums
{
    public enum TimewarpToInstanceEnum : byte
    {
        TimewarpToNow = 0,
        TimewarpToManeuver = 1,
        TimewarpToBurn = 2,
        TimewarpToNextSOI = 3,
        TimewarpToApoapsis = 4,
        TimewarpToPeriapsis = 5,
        TimewarpToNextMorning = 6
    }
}
