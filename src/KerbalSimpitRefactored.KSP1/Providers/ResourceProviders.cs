using KerbalSimpitRefactored.Common;
using KerbalSimpitRefactored.Common.Interfaces;
using SimpitRefactored.Common.Core;
using SimpitRefactored.Unity.Common.Providers;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static partial class ReosurceProviders
    {
        public abstract class BaseResourceProvider<T> : GenericUpdateProvider<T>
            where T : unmanaged, ISimpitMessageData, IBasicResourceMessage
        {
            private readonly PartResourceDefinition _resource;

            public virtual string ResourceName { get; } = typeof(T).Name;

            public BaseResourceProvider()
            {
                _resource = PartResourceLibrary.Instance.GetDefinition(this.ResourceName);
            }

            protected override T GetOutgoingData()
            {
                T instance = new T();
                if (FlightGlobals.ActiveVessel == null)
                {
                    return instance;
                }

                FlightGlobals.ActiveVessel.GetConnectedResourceTotals(_resource.id, out double available, out double max);
                instance.Available = (float)available;
                instance.Max = (float)max;

                return instance;
            }
        }

        public class LiquidFuelProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.LiquidFuel> { }
        public class OxidizerProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.Oxidizer> { }
        public class SolidFuelProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.SolidFuel> { }
        public class MonoPropellantProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.MonoPropellant> { }
        public class ElectricChargeProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.ElectricCharge> { }
        public class OreProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.Ore> { }
        public class AblatorProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.Ablator> { }
        public class XenonGasProvider : BaseResourceProvider<KerbalSimpit.Messages.Data.XenonGas> { }
        public class EvaPropellantProvider : GenericUpdateProvider<KerbalSimpit.Messages.Data.EvaPropellant>
        {
            protected override KerbalSimpit.Messages.Data.EvaPropellant GetOutgoingData()
            {
                KerbalSimpit.Messages.Data.EvaPropellant instance = new KerbalSimpit.Messages.Data.EvaPropellant();

                if (FlightGlobals.ActiveVessel == null)
                {
                    return instance;
                }

                if (FlightGlobals.ActiveVessel.isEVA == false || FlightGlobals.ActiveVessel.evaController == null)
                {
                    return instance;
                }

                instance.Available = (float)FlightGlobals.ActiveVessel.evaController.Fuel;
                instance.Max = (float)FlightGlobals.ActiveVessel.evaController.FuelCapacity;

                return instance;
            }
        }
    }
}
