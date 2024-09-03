using KerbalSimpitRefactored.Common.Constants;
using SimpitRefactored.Core;

namespace KerbalSimpitRefactored.Common.Extensions
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
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.LiquidFuel>(MessageTypeIds.Outgoing.LiquidFuel);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.LiquidFuelStage>(MessageTypeIds.Outgoing.LiquidFuelStage);
            // simpit.Messages.RegisterOutogingType<Resource.Methane>(MessageTypeIds.Outgoing.Methane);
            // simpit.Messages.RegisterOutogingType<Resource.MethaneStage>(MessageTypeIds.Outgoing.MethaneStage);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Oxidizer>(MessageTypeIds.Outgoing.Oxidizer);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.OxidizerStage>(MessageTypeIds.Outgoing.OxidizerStage);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.SolidFuel>(MessageTypeIds.Outgoing.SolidFuel);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.SolidFuelStage>(MessageTypeIds.Outgoing.SolidFuelStage);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.XenonGas>(MessageTypeIds.Outgoing.XenonGas);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.XenonGasStage>(MessageTypeIds.Outgoing.XenonGasStage);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.MonoPropellant>(MessageTypeIds.Outgoing.MonoPropellant);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.EvaPropellant>(MessageTypeIds.Outgoing.EvaPropellant);

            // Vessel Resources
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.ElectricCharge>(MessageTypeIds.Outgoing.ElectricCharge);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Ore>(MessageTypeIds.Outgoing.Ore);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Ablator>(MessageTypeIds.Outgoing.Ablator);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.AblatorStage>(MessageTypeIds.Outgoing.AblatorStage);

            // Vessel Movement/Postion
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Altitude>(MessageTypeIds.Outgoing.Altitude);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.VesselVelocity>(MessageTypeIds.Outgoing.Velocity);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.AirSpeed>(MessageTypeIds.Outgoing.Airspeed);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Apsides>(MessageTypeIds.Outgoing.Apsides);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.ApsidesTime>(MessageTypeIds.Outgoing.ApsidesTime);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Maneuver>(MessageTypeIds.Outgoing.ManeuverData);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.SAS>(MessageTypeIds.Outgoing.SASInfo);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.OrbitInfo>(MessageTypeIds.Outgoing.OrbitInfo);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.VesselRotation>(MessageTypeIds.Outgoing.Rotation);

            // Vessel Details
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.ActionGroups>(MessageTypeIds.Outgoing.ActionGroups);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.DeltaV>(MessageTypeIds.Outgoing.DeltaV);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.DeltaVEnv>(MessageTypeIds.Outgoing.DeltaVEnv);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.BurnTime>(MessageTypeIds.Outgoing.BurnTime);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.CustomActionGroups>(MessageTypeIds.Outgoing.CustomActionGroups);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.TempLimit>(MessageTypeIds.Outgoing.TempLimit);

            // External Environment
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Target>(MessageTypeIds.Outgoing.TargetInfo);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.SoI>(MessageTypeIds.Outgoing.SoIName);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.SceneChange>(MessageTypeIds.Outgoing.SceneChange);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.FlightStatus>(MessageTypeIds.Outgoing.FlightStatus);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.AtmoCondition>(MessageTypeIds.Outgoing.AtmoCondition);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.Vessel>(MessageTypeIds.Outgoing.VesselName);
            simpit.Messages.RegisterOutogingType<KerbalSimpit.Messages.Data.VesselChange>(MessageTypeIds.Outgoing.VesselChange);
            #endregion

            #region Incoming Messages
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.CustomActionGroupEnable>(MessageTypeIds.Incoming.CustomActionGroupEnable);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.CustomActionGroupDisable>(MessageTypeIds.Incoming.CustomActionGroupDisable);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.CustomActionGroupToggle>(MessageTypeIds.Incoming.CustomActionGroupToggle);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.ActionGroupActivate>(MessageTypeIds.Incoming.ActionGroupActivate);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.ActionGroupDeactivate>(MessageTypeIds.Incoming.ActionGroupDeactivate);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.ActionGroupToggle>(MessageTypeIds.Incoming.ActionGroupToggle);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.Rotation>(MessageTypeIds.Incoming.Rotation);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.Translation>(MessageTypeIds.Incoming.Translation);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.Wheel>(MessageTypeIds.Incoming.WheelControl);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.Throttle>(MessageTypeIds.Incoming.Throttle);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.AutopilotMode>(MessageTypeIds.Incoming.AutopilotMode);

            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.CameraMode>(MessageTypeIds.Incoming.CameraMode);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.CameraRotation>(MessageTypeIds.Incoming.CameraRotation);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.CameraTranslation>(MessageTypeIds.Incoming.CameraTranslation);

            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.WarpChange>(MessageTypeIds.Incoming.WarpChange);
            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.TimewarpTo>(MessageTypeIds.Incoming.TimewarpTo);

            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.NavballMode>(MessageTypeIds.Incoming.NavballMode);

            simpit.Messages.RegisterIncomingType<KerbalSimpit.Messages.Commands.KeyboardEmulator>(MessageTypeIds.Incoming.KeyboardEmulator);
            #endregion

            return simpit;
        }
    }
}
