using KerbalSimpitRefactored.Common;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static partial class VesselProviders
    {
        public class RotationCommandProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.FlightCtrlRotation>
        {
            protected override KerbalSimpit.Messages.Data.FlightCtrlRotation GetOutgoingData()
            {
                return new KerbalSimpit.Messages.Data.FlightCtrlRotation()
                {
                    Pitch = (short)(this.controller.LastFlightCtrlState.pitch * short.MaxValue),
                    Yaw = (short)(this.controller.LastFlightCtrlState.yaw * short.MaxValue),
                    Roll = (short)(this.controller.LastFlightCtrlState.roll * short.MaxValue)
                };
            }
        }

        public class TranslationCommandProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.FlightCtrlTranslation>
        {
            protected override KerbalSimpit.Messages.Data.FlightCtrlTranslation GetOutgoingData()
            {
                return new KerbalSimpit.Messages.Data.FlightCtrlTranslation()
                {
                    X = (short)(this.controller.LastFlightCtrlState.X * short.MaxValue),
                    Y = (short)(this.controller.LastFlightCtrlState.Y * short.MaxValue),
                    Z = (short)(this.controller.LastFlightCtrlState.Z * short.MaxValue)
                };
            }
        }

        public class WheelCommandProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.FlightCtrlWheel>
        {
            protected override KerbalSimpit.Messages.Data.FlightCtrlWheel GetOutgoingData()
            {
                return new KerbalSimpit.Messages.Data.FlightCtrlWheel()
                {
                    Steer = (short)(this.controller.LastFlightCtrlState.wheelSteer * short.MaxValue),
                    Throttle = (short)(this.controller.LastFlightCtrlState.wheelThrottle * short.MaxValue)
                };
            }
        }

        public class ThrottleCommandProvider : BaseVesselProvider<KerbalSimpit.Messages.Data.FlightCtrlThrottle>
        {
            protected override KerbalSimpit.Messages.Data.FlightCtrlThrottle GetOutgoingData()
            {
                return new KerbalSimpit.Messages.Data.FlightCtrlThrottle()
                {
                    Value = (short)(this.controller.LastFlightCtrlState.mainThrottle * short.MaxValue)
                };
            }
        }
    }
}
