using KerbalSimpitRefactored.Common.Enums;
using KerbalSimpitRefactored.Common.Interfaces;
using SimpitRefactored.Common.Core;
using SimpitRefactored.Common.Core.Utilities;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common
{
    public static class KerbalSimpit
    {
        public static class Messages
        {
            public static class Data
            {
                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Ablator : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct AblatorStage : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct ActionGroups : ISimpitMessageData
                {
                    public ActionGroupFlags Flags { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct AirSpeed : ISimpitMessageData
                {
                    public float IndicatedAirSpeed { get; set; }
                    public float MachNumber { get; set; }
                    public float GeeForce { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Altitude : ISimpitMessageData
                {
                    public float Alt { get; set; }
                    public float SurfAlt { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Apsides : ISimpitMessageData
                {
                    public float Periapsis { get; set; }
                    public float Apoapsis { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct ApsidesTime : ISimpitMessageData
                {
                    public int Periapsis { get; set; }
                    public int Apoapsis { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct AtmoCondition : ISimpitMessageData
                {
                    public AtmoCharacteristicsFlags AtmoCharacteristics;
                    public float AirDensity;
                    public float Temperature;
                    public float Pressure;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct BurnTime : ISimpitMessageData
                {
                    public float StageBurnTime { get; set; }
                    public float TotalBurnTime { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe struct CustomActionGroups : ISimpitMessageData
                {
                    public const int Length = 32;
                    public fixed byte Status[KerbalSimpit.Messages.Data.CustomActionGroups.Length];

                    public bool Equals(KerbalSimpit.Messages.Data.CustomActionGroups obj)
                    {
                        for (int i = 0; i < Length; i++)
                        {
                            if (Status[i] != obj.Status[i])
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct DeltaV : ISimpitMessageData
                {
                    public float StageDeltaV { get; set; }
                    public float TotalDeltaV { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct DeltaVEnv : ISimpitMessageData
                {
                    public float StageDeltaVASL { get; set; }
                    public float TotalDeltaVASL { get; set; }
                    public float StageDeltaVVac { get; set; }
                    public float TotalDeltaVVac { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct ElectricCharge : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct EvaPropellant : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct FlightCtrlRotation : ISimpitMessageData
                {
                    public short Pitch { get; set; }
                    public short Roll { get; set; }
                    public short Yaw { get; set; }
                    public byte Mask { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct FlightCtrlThrottle : ISimpitMessageData
                {
                    public short Value { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct FlightCtrlTranslation : ISimpitMessageData
                {
                    public short X { get; set; }
                    public short Y { get; set; }
                    public short Z { get; set; }
                    public byte Mask { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct FlightCtrlWheel : ISimpitMessageData
                {
                    public short Steer { get; set; }
                    public short Throttle { get; set; }
                    public byte Mask { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct FlightStatus : ISimpitMessageData
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

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct LiquidFuel : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct LiquidFuelStage : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Maneuver : ISimpitMessageData
                {
                    public float TimeToNextManeuver { get; set; }
                    public float DeltaVNextManeuver { get; set; }
                    public float DurationNextManeuver { get; set; }
                    public float DeltaVTotal { get; set; }
                    public float HeadingNextManeuver { get; set; }
                    public float PitchNextManeuver { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Methane : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct MethaneStage : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct MonoPropellant : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct OrbitInfo : ISimpitMessageData
                {
                    public float Eccentricity { get; set; }
                    public float SemiMajorAxis { get; set; }
                    public float Inclination { get; set; }
                    public float LongAscendingNode { get; set; }
                    public float ArgPeriapsis { get; set; }
                    public float TrueAnomaly { get; set; }
                    public float MeanAnomaly { get; set; }
                    public float Period { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Ore : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Oxidizer : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct OxidizerStage : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct SAS : ISimpitMessageData
                {
                    public AutoPilotModeEnum CurrentSASMode { get; set; }
                    public AutoPilotModeFlags SASModeAvailability { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct SceneChange : ISimpitMessageData
                {
                    public SceneChangeTypeEnum Type;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct SoI : ISimpitMessageData
                {
                    public FixedString Name { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct SolidFuel : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct SolidFuelStage : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Target : ISimpitMessageData
                {
                    public float Distance { get; set; }
                    public float Velocity { get; set; }
                    public float Heading { get; set; }
                    public float Pitch { get; set; }
                    public float VelocityHeading { get; set; }
                    public float VelocityPitch { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct TempLimit : ISimpitMessageData
                {
                    public byte TempLimitPercentage { get; set; }
                    public byte SkinTempLimitPercentage { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct VesselChange : ISimpitMessageData
                {
                    public VesselChangeTypeEnum Type { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Vessel : ISimpitMessageData
                {
                    public FixedString Name { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct VesselRotation : ISimpitMessageData
                {
                    public float Heading { get; set; }
                    public float Pitch { get; set; }
                    public float Roll { get; set; }
                    public float OrbitalVelocityHeading { get; set; }
                    public float OrbitalVelocityPitch { get; set; }
                    public float SurfaceVelocityHeading { get; set; }
                    public float SurfaceVelocityPitch { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct VesselVelocity : ISimpitMessageData
                {
                    public float Orbital { get; set; }
                    public float Surface { get; set; }
                    public float Vertical { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct XenonGas : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct XenonGasStage : IBasicResourceMessage
                {
                    public float Max { get; set; }
                    public float Available { get; set; }
                }
            }

            public static class Commands
            {
                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct ActionGroupActivate : ISimpitMessageData
                {
                    public ActionGroupFlags Flags { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct ActionGroupDeactivate : ISimpitMessageData
                {
                    public ActionGroupFlags Flags { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct ActionGroupToggle : ISimpitMessageData
                {
                    public ActionGroupFlags Flags { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct AutopilotMode : ISimpitMessageData
                {
                    public AutoPilotModeEnum Value { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct CameraMode : ISimpitMessageData
                {
                    public CameraModeEnum Value { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct CameraRotation : ISimpitMessageData
                {
                    public short Pitch;
                    public short Roll;
                    public short Yaw;
                    public short Zoom;
                    public byte Mask;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct CameraTranslation : ISimpitMessageData
                {
                    public short X;
                    public short Y;
                    public short Z;
                    public byte Mask;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe struct CustomActionGroupDisable : ISimpitMessageData
                {
                    public FixedBuffer GroupIds;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe struct CustomActionGroupEnable : ISimpitMessageData
                {
                    public FixedBuffer GroupIds;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public unsafe struct CustomActionGroupToggle : ISimpitMessageData
                {
                    public FixedBuffer GroupIds;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct CustomAxis : ISimpitMessageData
                {
                    public short Custom1 { get; set; }
                    public short Custom2 { get; set; }
                    public short Custom3 { get; set; }
                    public short Custom4 { get; set; }
                    public byte Mask { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct KeyboardEmulator : ISimpitMessageData
                {
                    public KeyboardEmulatorModifierFlags Modifier;
                    public short Key;
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct NavballMode : ISimpitMessageData
                {
                    // TODO: Check if any data is actually transmitted with this message
                    // If there is, it seems unnecessary
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Rotation : ISimpitMessageData
                {
                    public short Pitch { get; set; }
                    public short Roll { get; set; }
                    public short Yaw { get; set; }
                    public byte Mask { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Throttle : ISimpitMessageData
                {
                    public short Value { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct TimewarpTo : ISimpitMessageData
                {
                    public TimewarpToInstanceEnum Instant;
                    public float Delay; // negative for warping before the instant
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Translation : ISimpitMessageData
                {
                    public short X { get; set; }
                    public short Y { get; set; }
                    public short Z { get; set; }
                    public byte Mask { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct WarpChange : ISimpitMessageData
                {
                    public WarpChangeRateEnum Rate { get; set; }
                }

                [StructLayout(LayoutKind.Sequential, Pack = 1)]
                public struct Wheel : ISimpitMessageData
                {
                    public short Steer { get; set; }
                    public short Throttle { get; set; }
                    public byte Mask { get; set; }
                }
            }
        }
    }
}