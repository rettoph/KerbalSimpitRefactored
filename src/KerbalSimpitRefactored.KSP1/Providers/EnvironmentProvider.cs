using KerbalSimpitRefactored.Common;
using KerbalSimpitRefactored.Common.Enums;
using KerbalSimpitRefactored.Unity.KSP1.Helpers;
using SimpitRefactored.Common.Core.Utilities;
using SimpitRefactored.Unity.Common;
using SimpitRefactored.Unity.Common.Providers;
using System;
using UnityEngine;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static class EnvironmentProvider
    {
        public class TargetInfoProvider : GenericUpdateProvider<KerbalSimpit.Messages.Data.Target>
        {
            protected override bool ShouldCleanOutgoingData()
            {
                if (FlightGlobals.fetch.VesselTarget == null)
                {
                    return false;
                }

                if (FlightGlobals.fetch.VesselTarget == null)
                {
                    return false;
                }

                if (FlightGlobals.ActiveVessel == null)
                {
                    return false;
                }

                if (FlightGlobals.ship_tgtVelocity == null)
                {
                    return false;
                }

                if (FlightGlobals.ActiveVessel.targetObject == null)
                {
                    return false;
                }

                if (FlightGlobals.fetch.VesselTarget.GetTransform() == null)
                {
                    return false;
                }

                if (FlightGlobals.ActiveVessel.transform == null)
                {
                    return false;
                }

                return base.ShouldCleanOutgoingData();
            }

            protected override KerbalSimpit.Messages.Data.Target GetOutgoingData()
            {
                Vector3 targetDirection = FlightGlobals.ActiveVessel.targetObject.GetTransform().position - FlightGlobals.ActiveVessel.transform.position;

                TelemetryHelper.WorldVecToNavHeading(FlightGlobals.ActiveVessel, targetDirection, out float heading, out float pitch);
                TelemetryHelper.WorldVecToNavHeading(FlightGlobals.ActiveVessel, FlightGlobals.ship_tgtVelocity, out float velocityHeading, out float velocityPitch);

                return new KerbalSimpit.Messages.Data.Target()
                {
                    Distance = (float)Vector3.Distance(FlightGlobals.fetch.VesselTarget.GetTransform().position, FlightGlobals.ActiveVessel.transform.position),
                    Velocity = (float)FlightGlobals.ship_tgtVelocity.magnitude,
                    Heading = heading,
                    Pitch = pitch,
                    VelocityHeading = velocityHeading,
                    VelocityPitch = velocityHeading
                };
            }
        }

        public class SoINameProvider : GenericUpdateProvider<KerbalSimpit.Messages.Data.SoI>
        {
            protected override bool ShouldCleanOutgoingData()
            {
                if (FlightGlobals.ActiveVessel == null)
                {
                    return false;
                }

                if (FlightGlobals.ActiveVessel.orbit == null)
                {
                    return false;
                }

                if (FlightGlobals.ActiveVessel.orbit.referenceBody == null)
                {
                    return false;
                }

                return base.ShouldCleanOutgoingData();
            }

            protected override KerbalSimpit.Messages.Data.SoI GetOutgoingData()
            {
                return new KerbalSimpit.Messages.Data.SoI()
                {
                    Name = new FixedString(FlightGlobals.ActiveVessel.orbit.referenceBody.bodyName)
                };
            }
        }

        [KSPAddon(KSPAddon.Startup.Flight, false)]
        public class SceneChangeProvider : SimpitBehaviour
        {
            public override void Start()
            {
                base.Start();

                this.simpit.SetOutgoingData(new KerbalSimpit.Messages.Data.SceneChange()
                {
                    Type = SceneChangeTypeEnum.Flight
                }, true);
            }

            public override void OnDestroy()
            {
                this.simpit.SetOutgoingData(new KerbalSimpit.Messages.Data.SceneChange()
                {
                    Type = SceneChangeTypeEnum.NotFlight
                }, true);
            }
        }

        public class FlightStatusProvider : GenericUpdateProvider<KerbalSimpit.Messages.Data.FlightStatus>
        {
            protected override bool ShouldCleanOutgoingData()
            {
                if (FlightGlobals.ActiveVessel == null)
                {
                    return false;
                }

                if (TimeWarp.fetch == null)
                {
                    return false;
                }

                return base.ShouldCleanOutgoingData();
            }

            protected override KerbalSimpit.Messages.Data.FlightStatus GetOutgoingData()
            {
                KerbalSimpit.Messages.Data.FlightStatus flightStatus = new KerbalSimpit.Messages.Data.FlightStatus
                {
                    Status = 0
                };

                if (HighLogic.LoadedSceneIsFlight) flightStatus.Status |= FlightStatusFlags.IsInFlight;
                if (FlightGlobals.ActiveVessel.isEVA) flightStatus.Status |= FlightStatusFlags.IsEva;
                if (FlightGlobals.ActiveVessel.IsRecoverable) flightStatus.Status |= FlightStatusFlags.IsRecoverable;
                if (TimeWarp.fetch.Mode == TimeWarp.Modes.LOW) flightStatus.Status |= FlightStatusFlags.IsInAtmoTW;
                if (FlightGlobals.fetch.VesselTarget != null) flightStatus.Status |= FlightStatusFlags.HasTargetSet;

                switch (FlightGlobals.ActiveVessel.CurrentControlLevel)
                {
                    case Vessel.ControlLevel.NONE:
                        break;
                    case Vessel.ControlLevel.PARTIAL_UNMANNED:
                        flightStatus.Status |= FlightStatusFlags.ComnetControlLevel0;
                        break;
                    case Vessel.ControlLevel.PARTIAL_MANNED:
                        flightStatus.Status |= FlightStatusFlags.ComnetControlLevel1;
                        break;
                    case Vessel.ControlLevel.FULL:
                        flightStatus.Status |= FlightStatusFlags.ComnetControlLevel0;
                        flightStatus.Status |= FlightStatusFlags.ComnetControlLevel1;
                        break;
                }

                flightStatus.VesselSituation = (byte)FlightGlobals.ActiveVessel.situation;
                flightStatus.CurrentTWIndex = (byte)TimeWarp.fetch.current_rate_index;
                flightStatus.CrewCapacity = (byte)Math.Min(byte.MaxValue, FlightGlobals.ActiveVessel.GetCrewCapacity());
                flightStatus.CrewCount = (byte)Math.Min(byte.MaxValue, FlightGlobals.ActiveVessel.GetCrewCount());

                if (FlightGlobals.ActiveVessel.connection == null)
                {
                    flightStatus.CommNetSignalStrenghPercentage = 0;
                }
                else
                {
                    flightStatus.CommNetSignalStrenghPercentage = (byte)Math.Round(100 * FlightGlobals.ActiveVessel.connection.SignalStrength);
                }

                flightStatus.CurrentStage = (byte)Math.Min(255, FlightGlobals.ActiveVessel.currentStage);
                flightStatus.VesselType = (byte)FlightGlobals.ActiveVessel.vesselType;

                return flightStatus;
            }
        }

        public class AtmoConditionProvider : GenericUpdateProvider<KerbalSimpit.Messages.Data.AtmoCondition>
        {
            protected override bool ShouldCleanOutgoingData()
            {
                if (FlightGlobals.ActiveVessel == null)
                {
                    return false;
                }

                if (FlightGlobals.ActiveVessel.mainBody == null)
                {
                    return false;
                }

                return base.ShouldCleanOutgoingData();
            }

            protected override KerbalSimpit.Messages.Data.AtmoCondition GetOutgoingData()
            {
                KerbalSimpit.Messages.Data.AtmoCondition atmoCondition = new KerbalSimpit.Messages.Data.AtmoCondition();

                Vessel vessel = FlightGlobals.ActiveVessel;
                CelestialBody body = FlightGlobals.ActiveVessel.mainBody;

                if (body.atmosphere == false)
                {
                    return atmoCondition;
                }

                atmoCondition.AtmoCharacteristics |= AtmoCharacteristicsFlags.HasAtmosphere;
                if (body.atmosphereContainsOxygen) atmoCondition.AtmoCharacteristics |= AtmoCharacteristicsFlags.HasOxygen;
                if (body.atmosphereDepth >= vessel.altitude) atmoCondition.AtmoCharacteristics |= AtmoCharacteristicsFlags.IsVesselInAtmosphere;

                atmoCondition.Temperature = (float)body.GetTemperature(vessel.altitude);
                atmoCondition.Pressure = (float)body.GetPressure(vessel.altitude);
                atmoCondition.AirDensity = (float)body.GetDensity(body.GetPressure(vessel.altitude), body.GetTemperature(vessel.altitude));

                FlightGlobals.ActiveVessel.mainBody.GetFullTemperature(FlightGlobals.ActiveVessel.CoMD);

                return atmoCondition;
            }
        }

        public class VesselNameProvider : GenericUpdateProvider<KerbalSimpit.Messages.Data.Vessel>
        {
            protected override bool ShouldCleanOutgoingData()
            {
                if (FlightGlobals.ActiveVessel == null)
                {
                    return false;
                }

                return base.ShouldCleanOutgoingData();
            }

            protected override KerbalSimpit.Messages.Data.Vessel GetOutgoingData()
            {
                return new KerbalSimpit.Messages.Data.Vessel()
                {
                    Name = new FixedString(FlightGlobals.ActiveVessel.GetDisplayName())
                };
            }
        }

        [KSPAddon(KSPAddon.Startup.Flight, false)]
        public class VesselChangeProvider : SimpitBehaviour
        {
            public override void Start()
            {
                base.Start();

                GameEvents.onVesselDocking.Add(this.VesselDockingHandler);
                GameEvents.onVesselsUndocking.Add(this.VesselUndockingHandler);
                GameEvents.onVesselSwitching.Add(this.VesselSwitchingHandler);
            }

            public override void OnDestroy()
            {
                base.OnDestroy();

                GameEvents.onVesselDocking.Remove(this.VesselDockingHandler);
                GameEvents.onVesselsUndocking.Remove(this.VesselUndockingHandler);
                GameEvents.onVesselSwitching.Remove(this.VesselSwitchingHandler);
            }

            private void VesselDockingHandler(uint data0, uint data1)
            {
                this.simpit.SetOutgoingData(new KerbalSimpit.Messages.Data.VesselChange()
                {
                    Type = VesselChangeTypeEnum.Docking
                });
            }

            private void VesselUndockingHandler(Vessel data0, Vessel data1)
            {
                this.simpit.SetOutgoingData(new KerbalSimpit.Messages.Data.VesselChange()
                {
                    Type = VesselChangeTypeEnum.Undocking
                });
            }

            private void VesselSwitchingHandler(Vessel data0, Vessel data1)
            {
                this.simpit.SetOutgoingData(new KerbalSimpit.Messages.Data.VesselChange()
                {
                    Type = VesselChangeTypeEnum.Switching
                });
            }
        }
    }
}
