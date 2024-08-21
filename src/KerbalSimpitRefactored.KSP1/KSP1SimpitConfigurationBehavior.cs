using KerbalSimpitRefactored.Core.KSP.Extensions;
using SimpitRefactored.Core;
using SimpitRefactored.Unity.Common;

namespace KerbalSimpitRefactored.Unity.KSP1
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class KSP1SimpitConfigurationBehavior : SimpitConfigurationBehavior
    {
        protected override Simpit ConfigureSimpit(Simpit simpit)
        {
            return simpit.RegisterKerbal();
        }
    }
}
