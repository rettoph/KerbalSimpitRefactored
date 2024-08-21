using KerbalSimpitRefactored.Common.Messages;
using KerbalSimpitRefactored.Common.Messages.Enums;
using KerbalSimpitRefactored.Common.Messages.Utilities;
using KerbalSimpitRefactored.Unity.KSP1.Helpers;
using System;
using UnityEngine;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static partial class VesselProviders
    {
        public class AltitudeProvider : BaseVesselProvider<AltitudeDataMessage>
        {
            protected override AltitudeDataMessage GetOutgoingData()
            {
                return new AltitudeDataMessage()
                {
                    Alt = (float)FlightGlobals.ActiveVessel.altitude,
                    SurfAlt = (float)FlightGlobals.ActiveVessel.radarAltitude
                };
            }
        }

        public class ApsidesProvider : BaseVesselProvider<ApsidesDataMessage>
        {
            protected override ApsidesDataMessage GetOutgoingData()
            {
                return new ApsidesDataMessage()
                {
                    Apoapsis = (float)FlightGlobals.ActiveVessel.orbit.ApA,
                    Periapsis = (float)FlightGlobals.ActiveVessel.orbit.PeA
                };
            }
        }

        public class ApsidesTimeProvider : BaseVesselProvider<ApsidesTimeDataMessage>
        {
            protected override ApsidesTimeDataMessage GetOutgoingData()
            {
                return new ApsidesTimeDataMessage()
                {
                    Apoapsis = (int)FlightGlobals.ActiveVessel.orbit.timeToAp,
                    Periapsis = (int)FlightGlobals.ActiveVessel.orbit.timeToPe
                };
            }
        }

        public class VelocityProvider : BaseVesselProvider<VesselVelocityDataMessage>
        {
            protected override VesselVelocityDataMessage GetOutgoingData()
            {
                return new VesselVelocityDataMessage()
                {
                    Orbital = (float)FlightGlobals.ActiveVessel.obt_speed,
                    Surface = (float)FlightGlobals.ActiveVessel.srfSpeed,
                    Vertical = (float)FlightGlobals.ActiveVessel.verticalSpeed,
                };
            }
        }

        public class RotationProvider : BaseVesselProvider<VesselRotationDataMessage>
        {
            protected override VesselRotationDataMessage GetOutgoingData()
            {
                // Code from KSPIO to compute angles and velocities https://github.com/zitron-git/KSPSerialIO/blob/062d97e892077ea14737f5e79268c0c4d067f5b6/KSPSerialIO/KSPIO.cs#L929-L971
                Vector3d CoM, north, up, east;
                CoM = FlightGlobals.ActiveVessel.CoM;
                up = (CoM - FlightGlobals.ActiveVessel.mainBody.position).normalized;
                north = Vector3d.Exclude(up, (FlightGlobals.ActiveVessel.mainBody.position + FlightGlobals.ActiveVessel.mainBody.transform.up * (float)FlightGlobals.ActiveVessel.mainBody.Radius) - CoM).normalized;
                east = Vector3d.Cross(up, north);

                Vector3d attitude = Quaternion.Inverse(Quaternion.Euler(90, 0, 0) * Quaternion.Inverse(FlightGlobals.ActiveVessel.GetTransform().rotation) * Quaternion.LookRotation(north, up)).eulerAngles;

                TelemetryHelper.WorldVecToNavHeading(FlightGlobals.ActiveVessel, FlightGlobals.ActiveVessel.srf_velocity.normalized, out float surfaceVelocityHeading, out float surfaceVelocityPitch);
                TelemetryHelper.WorldVecToNavHeading(FlightGlobals.ActiveVessel, FlightGlobals.ActiveVessel.obt_velocity.normalized, out float orbitalVelocityHeading, out float orbitalVelocityPitch);

                return new VesselRotationDataMessage()
                {
                    Roll = (float)((attitude.z > 180) ? (attitude.z - 360.0) : attitude.z),
                    Pitch = (float)((attitude.x > 180) ? (360.0 - attitude.x) : -attitude.x),
                    Heading = (float)attitude.y,
                    SurfaceVelocityHeading = surfaceVelocityHeading,
                    SurfaceVelocityPitch = surfaceVelocityPitch,
                    OrbitalVelocityHeading = orbitalVelocityHeading,
                    OrbitalVelocityPitch = orbitalVelocityPitch
                };
            }
        }

        public class OrbitInfoProvider : BaseVesselProvider<OrbitInfoDataMessage>
        {
            protected override OrbitInfoDataMessage GetOutgoingData()
            {
                return new OrbitInfoDataMessage()
                {
                    Eccentricity = (float)FlightGlobals.ActiveVessel.orbit.eccentricity,
                    SemiMajorAxis = (float)FlightGlobals.ActiveVessel.orbit.semiMajorAxis,
                    Inclination = (float)FlightGlobals.ActiveVessel.orbit.inclination,
                    LongAscendingNode = (float)FlightGlobals.ActiveVessel.orbit.LAN,
                    ArgPeriapsis = (float)FlightGlobals.ActiveVessel.orbit.argumentOfPeriapsis,
                    TrueAnomaly = (float)FlightGlobals.ActiveVessel.orbit.trueAnomaly,
                    MeanAnomaly = (float)FlightGlobals.ActiveVessel.orbit.meanAnomaly,
                    Period = (float)FlightGlobals.ActiveVessel.orbit.period,
                };
            }
        }

        public class AirspeedProvider : BaseVesselProvider<AirSpeedDataMessage>
        {
            protected override AirSpeedDataMessage GetOutgoingData()
            {
                return new AirSpeedDataMessage()
                {
                    IndicatedAirSpeed = (float)FlightGlobals.ActiveVessel.indicatedAirSpeed,
                    MachNumber = (float)FlightGlobals.ActiveVessel.mach,
                    GeeForce = (float)FlightGlobals.ActiveVessel.geeForce,
                };
            }
        }

        public class ManeuverProvider : BaseVesselProvider<ManeuverDataMessage>
        {
            protected override ManeuverDataMessage GetOutgoingData()
            {
                var maneuver = new ManeuverDataMessage();
                maneuver.TimeToNextManeuver = 0.0f;
                maneuver.DeltaVNextManeuver = 0.0f;
                maneuver.DurationNextManeuver = 0.0f;
                maneuver.DeltaVTotal = 0.0f;
                maneuver.HeadingNextManeuver = 0.0f;
                maneuver.PitchNextManeuver = 0.0f;

                if (FlightGlobals.ActiveVessel.patchedConicSolver != null)
                {
                    if (FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes != null)
                    {
                        System.Collections.Generic.List<ManeuverNode> maneuverNodes = FlightGlobals.ActiveVessel.patchedConicSolver.maneuverNodes;

                        if (maneuverNodes.Count > 0)
                        {
                            maneuver.TimeToNextManeuver = (float)(maneuverNodes[0].UT - Planetarium.GetUniversalTime());
                            maneuver.DeltaVNextManeuver = (float)maneuverNodes[0].GetPartialDv().magnitude;

                            TelemetryHelper.WorldVecToNavHeading(FlightGlobals.ActiveVessel, maneuverNodes[0].GetBurnVector(maneuverNodes[0].patch), out float headingNextManeuver, out float pitchNextManeuver);
                            maneuver.HeadingNextManeuver = headingNextManeuver;
                            maneuver.PitchNextManeuver = pitchNextManeuver;

                            DeltaVStageInfo currentStageInfo = getCurrentStageDeltaV();
                            if (currentStageInfo != null)
                            {
                                //Old method, use a simple crossmultiplication to compute the estimated burn time based on the current stage only
                                //myManeuver.durationNextManeuver = (float)(maneuvers[0].DeltaV.magnitude * currentStageInfo.stageBurnTime) / currentStageInfo.deltaVActual;

                                // The estimation based on the startBurnIn seems to be more accurate than using the previous method of crossmultiplication
                                maneuver.DurationNextManeuver = (float)((maneuverNodes[0].UT - Planetarium.GetUniversalTime() - maneuverNodes[0].startBurnIn) * 2);
                            }

                            foreach (ManeuverNode maneuverNode in maneuverNodes)
                            {
                                maneuver.DeltaVTotal += (float)maneuverNode.DeltaV.magnitude;
                            }
                        }
                    }
                }

                return maneuver;
            }
        }

        public class SASInfoProvider : BaseVesselProvider<SASDataMessage>
        {
            protected override bool ShouldCleanOutgoingData()
            {
                if (base.ShouldCleanOutgoingData() == false)
                {
                    return false;
                }

                return FlightGlobals.ActiveVessel.Autopilot != null;
            }

            protected override SASDataMessage GetOutgoingData()
            {
                SASDataMessage sasInfo = new SASDataMessage();

                if (FlightGlobals.ActiveVessel.Autopilot.Enabled)
                {
                    sasInfo.CurrentSASMode = (AutoPilotModeEnum)FlightGlobals.ActiveVessel.Autopilot.Mode;
                }
                else
                {
                    sasInfo.CurrentSASMode = AutoPilotModeEnum.Disabled;
                }

                sasInfo.SASModeAvailability = 0;
                foreach (VesselAutopilot.AutopilotMode i in Enum.GetValues(typeof(VesselAutopilot.AutopilotMode)))
                {
                    if (FlightGlobals.ActiveVessel.Autopilot.CanSetMode(i))
                    {
                        sasInfo.SASModeAvailability |= AutoPilotHelper.ModeEnumToFlag((AutoPilotModeEnum)i);
                    }
                }

                return sasInfo;
            }
        }

        //Return the DeltaVStageInfo of the first stage to consider for deltaV and burn time computation
        //Can return null when no deltaV is available (for instance in EVA).
        private static DeltaVStageInfo getCurrentStageDeltaV()
        {
            if (FlightGlobals.ActiveVessel.VesselDeltaV == null)
            {
                return null; //This happen in EVA for instance.
            }
            DeltaVStageInfo currentStageInfo = null;

            try
            {
                if (FlightGlobals.ActiveVessel.currentStage == FlightGlobals.ActiveVessel.VesselDeltaV.OperatingStageInfo.Count)
                {
                    // Rocket has not taken off, use first stage with deltaV (to avoid stage of only stabilizer)
                    for (int i = FlightGlobals.ActiveVessel.VesselDeltaV.OperatingStageInfo.Count - 1; i >= 0; i--)
                    {
                        currentStageInfo = FlightGlobals.ActiveVessel.VesselDeltaV.GetStage(i);
                        if (currentStageInfo.deltaVActual > 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    currentStageInfo = FlightGlobals.ActiveVessel.VesselDeltaV.GetStage(FlightGlobals.ActiveVessel.currentStage);
                }
            }
            catch (NullReferenceException)
            {
                // This happens when reverting a flight.
                // FlightGlobals.ActiveVessel.VesselDeltaV.OperatingStageInfo is not null but using it produce a
                // NullReferenceException in KSP code. This is probably due to the fact that the rocket is not fully initialized.
            }

            return currentStageInfo;
        }
    }
}
