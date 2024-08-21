using KerbalSimpitRefactored.Common.Messages;
using KerbalSimpitRefactored.Common.Messages.Interfaces;
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

            public BaseResourceProvider(string resourceName = null)
            {
                if (resourceName == null)
                {
                    resourceName = typeof(T).Name;
                }

                _resource = PartResourceLibrary.Instance.GetDefinition(resourceName);
            }

            protected override T GetOutgoingData()
            {
                T instance = new T();

                FlightGlobals.ActiveVessel.GetConnectedResourceTotals(_resource.id, out double available, out double max);
                instance.Available = (float)available;
                instance.Max = (float)max;

                return instance;
            }
        }

        public class LiquidFuelProvider : BaseResourceProvider<LiquidFuelDataMessage> { }
        public class OxidizerProvider : BaseResourceProvider<OxidizerDataMessage> { }
        public class SolidFuelProvider : BaseResourceProvider<SolidFuelDataMessage> { }
        public class MonoPropellantProvider : BaseResourceProvider<MonoPropellantDataMessage> { }
        public class ElectricChargeProvider : BaseResourceProvider<ElectricChargeDataMessage> { }
        public class OreProvider : BaseResourceProvider<OreDataMessage> { }
        public class AblatorProvider : BaseResourceProvider<AblatorDataMessage> { }
        public class XenonGasProvider : BaseResourceProvider<XenonGasDataMessage> { }
    }
}
