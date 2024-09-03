using KerbalSimpitRefactored.Common;
using KerbalSimpitRefactored.Common.Enums;
using System;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static partial class VesselProviders
    {
        public class ActionGroupsProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.ActionGroups>
        {
            protected override KerbalSimpit.Messages.Data.ActionGroups GetOutgoingData()
            {
                ActionGroupFlags flags = ActionGroupFlags.None;
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.Stage])
                {
                    flags |= ActionGroupFlags.Stage;
                }
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.Gear])
                {
                    flags |= ActionGroupFlags.Gear;
                }
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.Light])
                {
                    flags |= ActionGroupFlags.Light;
                }
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.RCS])
                {
                    flags |= ActionGroupFlags.RCS;
                }
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.SAS])
                {
                    flags |= ActionGroupFlags.SAS;
                }
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.Brakes])
                {
                    flags |= ActionGroupFlags.Brakes;
                }
                if (FlightGlobals.ActiveVessel.ActionGroups[KSPActionGroup.Abort])
                {
                    flags |= ActionGroupFlags.Abort;
                }

                return new KerbalSimpit.Messages.Data.ActionGroups()
                {
                    Flags = flags
                };
            }
        }

        public class DeltaVEnvProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.DeltaVEnv>
        {
            protected override KerbalSimpit.Messages.Data.DeltaVEnv GetOutgoingData()
            {
                DeltaVStageInfo currentStageInfo = getCurrentStageDeltaV();
                if (currentStageInfo == null)
                {
                    return default;
                }

                return new KerbalSimpit.Messages.Data.DeltaVEnv()
                {
                    StageDeltaVASL = (float)currentStageInfo.deltaVatASL,
                    StageDeltaVVac = (float)currentStageInfo.deltaVinVac,
                    TotalDeltaVASL = (float)FlightGlobals.ActiveVessel.VesselDeltaV.TotalDeltaVASL,
                    TotalDeltaVVac = (float)FlightGlobals.ActiveVessel.VesselDeltaV.TotalDeltaVVac,
                };
            }
        }

        public class DeltaVProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.DeltaV>
        {
            protected override KerbalSimpit.Messages.Data.DeltaV GetOutgoingData()
            {
                DeltaVStageInfo currentStageInfo = getCurrentStageDeltaV();
                if (currentStageInfo == null)
                {
                    return default;
                }

                return new KerbalSimpit.Messages.Data.DeltaV()
                {
                    StageDeltaV = (float)currentStageInfo.deltaVActual,
                    TotalDeltaV = (float)FlightGlobals.ActiveVessel.VesselDeltaV.TotalDeltaVActual
                };
            }
        }

        public class BurnTimeProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.BurnTime>
        {
            protected override KerbalSimpit.Messages.Data.BurnTime GetOutgoingData()
            {
                DeltaVStageInfo currentStageInfo = getCurrentStageDeltaV();
                if (currentStageInfo == null)
                {
                    return default;
                }

                return new KerbalSimpit.Messages.Data.BurnTime()
                {
                    StageBurnTime = (float)currentStageInfo.stageBurnTime,
                    TotalBurnTime = (float)FlightGlobals.ActiveVessel.VesselDeltaV.TotalBurnTime
                };
            }
        }

        public class CustomActionGroupsProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.CustomActionGroups>
        {
            protected unsafe override KerbalSimpit.Messages.Data.CustomActionGroups GetOutgoingData()
            {
                KerbalSimpit.Messages.Data.CustomActionGroups result = new KerbalSimpit.Messages.Data.CustomActionGroups();

                for (int i = 1; i < this.controller.ActionGroupIDs.Length; i++) //Ignoring 0 since there is no Action Group 0
                {
                    if (FlightGlobals.ActiveVessel.ActionGroups[this.controller.ActionGroupIDs[i]])
                    {
                        result.Status[i / 8] |= (byte)(1 << (i % 8)); //Set the selected bit to 1
                    }
                }

                // TODO: Investigate AGX integration

                return result;
            }
        }

        public class TempLimitProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.TempLimit>
        {
            protected override KerbalSimpit.Messages.Data.TempLimit GetOutgoingData()
            {
                double maxTempPercentage = 0.0;
                double maxSkinTempPercentage = 0.0;

                // Iterate on a copy ?
                foreach (Part part in FlightGlobals.ActiveVessel.Parts)
                {
                    maxTempPercentage = Math.Max(maxTempPercentage, 100.0 * part.temperature / part.maxTemp);
                    maxSkinTempPercentage = Math.Max(maxSkinTempPercentage, 100.0 * part.skinTemperature / part.skinMaxTemp);
                }

                //Prevent the byte to overflow in case of extremely hot vessel
                if (maxTempPercentage > 255) maxTempPercentage = 255;
                if (maxSkinTempPercentage > 255) maxSkinTempPercentage = 255;

                return new KerbalSimpit.Messages.Data.TempLimit()
                {
                    TempLimitPercentage = (byte)Math.Round(maxTempPercentage),
                    SkinTempLimitPercentage = (byte)Math.Round(maxSkinTempPercentage),
                };
            }
        }
    }
}
