using KerbalSimpitRefactored.Common.Messages;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static partial class VesselProviders
    {
        public class RotationCommandProvider : BaseVesselProvider<FlightCtrlRotationDataMessage>
        {
            protected override FlightCtrlRotationDataMessage GetOutgoingData()
            {
                return new FlightCtrlRotationDataMessage()
                {
                    Pitch = (short)(this.controller.LastFlightCtrlState.pitch * short.MaxValue),
                    Yaw = (short)(this.controller.LastFlightCtrlState.yaw * short.MaxValue),
                    Roll = (short)(this.controller.LastFlightCtrlState.roll * short.MaxValue)
                };
            }
        }

        public class TranslationCommandProvider : BaseVesselProvider<FlightCtrlTranslationDataMessage>
        {
            protected override FlightCtrlTranslationDataMessage GetOutgoingData()
            {
                return new FlightCtrlTranslationDataMessage()
                {
                    X = (short)(this.controller.LastFlightCtrlState.X * short.MaxValue),
                    Y = (short)(this.controller.LastFlightCtrlState.Y * short.MaxValue),
                    Z = (short)(this.controller.LastFlightCtrlState.Z * short.MaxValue)
                };
            }
        }

        public class WheelCommandProvider : BaseVesselProvider<FlightCtrlWheelDataMessage>
        {
            protected override FlightCtrlWheelDataMessage GetOutgoingData()
            {
                return new FlightCtrlWheelDataMessage()
                {
                    Steer = (short)(this.controller.LastFlightCtrlState.wheelSteer * short.MaxValue),
                    Throttle = (short)(this.controller.LastFlightCtrlState.wheelThrottle * short.MaxValue)
                };
            }
        }

        public class ThrottleCommandProvider : BaseVesselProvider<FlightCtrlThrottleDataMessage>
        {
            protected override FlightCtrlThrottleDataMessage GetOutgoingData()
            {
                return new FlightCtrlThrottleDataMessage()
                {
                    Value = (short)(this.controller.LastFlightCtrlState.mainThrottle * short.MaxValue)
                };
            }
        }
    }
}
