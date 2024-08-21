using KerbalSimpitRefactored.Unity.KSP1.Controllers;
using SimpitRefactored.Common.Core;
using SimpitRefactored.Unity.Common.Providers;
using System;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    public static partial class VesselProviders
    {
        public abstract class BaseVesselProvider<T> : GenericUpdateProvider<T>
            where T : unmanaged, ISimpitMessageData
        {
            protected VesselController controller { get; private set; }

            public override void Start()
            {
                base.Start();

                this.controller = FindObjectOfType<VesselController>();

                if (this.controller == null)
                {
                    throw new InvalidOperationException($"{this.GetType().Name}::{nameof(Start)} - Unable to locate {nameof(VesselController)}");
                }
            }

            protected override bool ShouldCleanOutgoingData()
            {
                if (FlightGlobals.ActiveVessel == null)
                {
                    return false;
                }

                return base.ShouldCleanOutgoingData();
            }
        }
    }
}
