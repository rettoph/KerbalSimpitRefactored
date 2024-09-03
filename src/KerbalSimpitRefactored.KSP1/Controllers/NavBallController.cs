using KerbalSimpitRefactored.Common;
using SimpitRefactored.Core;
using SimpitRefactored.Core.Peers;
using SimpitRefactored.Unity.Common;

namespace KerbalSimpitRefactored.Unity.KSP1.Controllers
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class NavBallController : SimpitBehaviour,
        ISimpitMessageSubscriber<KerbalSimpit.Messages.Commands.NavballMode>
    {
        public void Process(SimpitPeer peer, ISimpitMessage<KerbalSimpit.Messages.Commands.NavballMode> message)
        {
            FlightGlobals.CycleSpeedModes();
        }
    }
}
