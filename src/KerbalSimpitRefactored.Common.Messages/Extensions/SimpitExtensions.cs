using KerbalSimpitRefactored.Common.Messages;
using KerbalSimpitRefactored.Common.Messages.Constants;
using KerbalSimpitRefactored.Core.KSP.Messages;
using SimpitRefactored.Core;

namespace KerbalSimpitRefactored.Core.KSP.Extensions
{
    public static class SimpitExtensions
    {
        public static Simpit RegisterKerbal(this Simpit simpit)
        {
            return simpit.RegisterKerbalMessages();
        }

        private static Simpit RegisterKerbalMessages(this Simpit simpit)
        {
            #region Outgoing Messaages
            // Propulsion Resources
            simpit.Messages.RegisterOutogingType<LiquidFuelDataMessage>(MessageTypeIds.Outgoing.LiquidFuel);
            simpit.Messages.RegisterOutogingType<LiquidFuelStageDataMessage>(MessageTypeIds.Outgoing.LiquidFuelStage);
            // simpit.Messages.RegisterOutogingType<Resource.Methane>(MessageTypeIds.Outgoing.Methane);
            // simpit.Messages.RegisterOutogingType<Resource.MethaneStage>(MessageTypeIds.Outgoing.MethaneStage);
            simpit.Messages.RegisterOutogingType<OxidizerDataMessage>(MessageTypeIds.Outgoing.Oxidizer);
            simpit.Messages.RegisterOutogingType<OxidizerStageDataMessage>(MessageTypeIds.Outgoing.OxidizerStage);
            simpit.Messages.RegisterOutogingType<SolidFuelDataMessage>(MessageTypeIds.Outgoing.SolidFuel);
            simpit.Messages.RegisterOutogingType<SolidFuelStageDataMessage>(MessageTypeIds.Outgoing.SolidFuelStage);
            simpit.Messages.RegisterOutogingType<XenonGasDataMessage>(MessageTypeIds.Outgoing.XenonGas);
            simpit.Messages.RegisterOutogingType<XenonGasStageDataMessage>(MessageTypeIds.Outgoing.XenonGasStage);
            simpit.Messages.RegisterOutogingType<MonoPropellantDataMessage>(MessageTypeIds.Outgoing.MonoPropellant);
            simpit.Messages.RegisterOutogingType<EvaPropellantDataMessage>(MessageTypeIds.Outgoing.EvaPropellant);

            // Vessel Resources
            simpit.Messages.RegisterOutogingType<ElectricChargeDataMessage>(MessageTypeIds.Outgoing.ElectricCharge);
            simpit.Messages.RegisterOutogingType<OreDataMessage>(MessageTypeIds.Outgoing.Ore);
            simpit.Messages.RegisterOutogingType<AblatorDataMessage>(MessageTypeIds.Outgoing.Ablator);
            simpit.Messages.RegisterOutogingType<AblatorStageDataMessage>(MessageTypeIds.Outgoing.AblatorStage);

            // Vessel Movement/Postion
            simpit.Messages.RegisterOutogingType<AltitudeDataMessage>(MessageTypeIds.Outgoing.Altitude);
            simpit.Messages.RegisterOutogingType<VesselVelocityDataMessage>(MessageTypeIds.Outgoing.Velocity);
            simpit.Messages.RegisterOutogingType<AirSpeedDataMessage>(MessageTypeIds.Outgoing.Airspeed);
            simpit.Messages.RegisterOutogingType<ApsidesDataMessage>(MessageTypeIds.Outgoing.Apsides);
            simpit.Messages.RegisterOutogingType<ApsidesTimeDataMessage>(MessageTypeIds.Outgoing.ApsidesTime);
            simpit.Messages.RegisterOutogingType<ManeuverDataMessage>(MessageTypeIds.Outgoing.ManeuverData);
            simpit.Messages.RegisterOutogingType<SASDataMessage>(MessageTypeIds.Outgoing.SASInfo);
            simpit.Messages.RegisterOutogingType<OrbitInfoDataMessage>(MessageTypeIds.Outgoing.OrbitInfo);
            simpit.Messages.RegisterOutogingType<VesselRotationDataMessage>(MessageTypeIds.Outgoing.Rotation);

            // Vessel Details
            simpit.Messages.RegisterOutogingType<ActionGroupsDataMessage>(MessageTypeIds.Outgoing.ActionGroups);
            simpit.Messages.RegisterOutogingType<DeltaVDataMessage>(MessageTypeIds.Outgoing.DeltaV);
            simpit.Messages.RegisterOutogingType<DeltaVEnvDataMessage>(MessageTypeIds.Outgoing.DeltaVEnv);
            simpit.Messages.RegisterOutogingType<BurnTimeDataMessage>(MessageTypeIds.Outgoing.BurnTime);
            simpit.Messages.RegisterOutogingType<CustomActionGroupsDataMessage>(MessageTypeIds.Outgoing.CustomActionGroups);
            simpit.Messages.RegisterOutogingType<TempLimitDataMessage>(MessageTypeIds.Outgoing.TempLimit);

            // External Environment
            simpit.Messages.RegisterOutogingType<TargetDataMessage>(MessageTypeIds.Outgoing.TargetInfo);
            simpit.Messages.RegisterOutogingType<SoIDataMessage>(MessageTypeIds.Outgoing.SoIName);
            simpit.Messages.RegisterOutogingType<SceneChangeDataMessage>(MessageTypeIds.Outgoing.SceneChange);
            simpit.Messages.RegisterOutogingType<FlightStatusDataMessage>(MessageTypeIds.Outgoing.FlightStatus);
            simpit.Messages.RegisterOutogingType<AtmoConditionDataMessage>(MessageTypeIds.Outgoing.AtmoCondition);
            simpit.Messages.RegisterOutogingType<VesselDataMessage>(MessageTypeIds.Outgoing.VesselName);
            simpit.Messages.RegisterOutogingType<VesselChangeDataMessage>(MessageTypeIds.Outgoing.VesselChange);
            #endregion

            #region Incoming Messages
            simpit.Messages.RegisterIncomingType<CustomActionGroupEnableCommandMessage>(MessageTypeIds.Incoming.CustomActionGroupEnable);
            simpit.Messages.RegisterIncomingType<CustomActionGroupDisableCommandMessage>(MessageTypeIds.Incoming.CustomActionGroupDisable);
            simpit.Messages.RegisterIncomingType<CustomActionGroupToggleCommandMessage>(MessageTypeIds.Incoming.CustomActionGroupToggle);
            simpit.Messages.RegisterIncomingType<ActionGroupActivateCommandMessage>(MessageTypeIds.Incoming.ActionGroupActivate);
            simpit.Messages.RegisterIncomingType<ActionGroupDeactivateCommandMessage>(MessageTypeIds.Incoming.ActionGroupDeactivate);
            simpit.Messages.RegisterIncomingType<ActionGroupToggleCommandMessage>(MessageTypeIds.Incoming.ActionGroupToggle);
            simpit.Messages.RegisterIncomingType<RotationCommandMessage>(MessageTypeIds.Incoming.Rotation);
            simpit.Messages.RegisterIncomingType<TranslationCommandMessage>(MessageTypeIds.Incoming.Translation);
            simpit.Messages.RegisterIncomingType<WheelCommandMessage>(MessageTypeIds.Incoming.WheelControl);
            simpit.Messages.RegisterIncomingType<ThrottleCommandMessage>(MessageTypeIds.Incoming.Throttle);
            simpit.Messages.RegisterIncomingType<AutopilotModeCommandMessage>(MessageTypeIds.Incoming.AutopilotMode);

            simpit.Messages.RegisterIncomingType<CameraModeCommandMessage>(MessageTypeIds.Incoming.CameraMode);
            simpit.Messages.RegisterIncomingType<CameraRotationCommandMessage>(MessageTypeIds.Incoming.CameraRotation);
            simpit.Messages.RegisterIncomingType<CameraTranslationCommandMessage>(MessageTypeIds.Incoming.CameraTranslation);

            simpit.Messages.RegisterIncomingType<WarpChangeCommandMessage>(MessageTypeIds.Incoming.WarpChange);
            simpit.Messages.RegisterIncomingType<TimewarpToCommandMessage>(MessageTypeIds.Incoming.TimewarpTo);

            simpit.Messages.RegisterIncomingType<NavballModeCommandMessage>(MessageTypeIds.Incoming.NavballMode);

            simpit.Messages.RegisterIncomingType<KeyboardEmulatorCommandMessage>(MessageTypeIds.Incoming.KeyboardEmulator);
            #endregion

            return simpit;
        }
    }
}
