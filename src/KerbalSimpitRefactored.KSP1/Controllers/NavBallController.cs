using KerbalSimpitRefactored.Core.KSP.Messages;
using SimpitRefactored.Core;
using SimpitRefactored.Core.Peers;
using SimpitRefactored.Unity.Common;

namespace KerbalSimpitRefactored.Unity.KSP1.Controllers
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class NavBallController : SimpitBehaviour,
        ISimpitMessageSubscriber<NavballModeCommandMessage>
    {
        public void Process(SimpitPeer peer, ISimpitMessage<NavballModeCommandMessage> message)
        {
            FlightGlobals.CycleSpeedModes();
        }
    }
}
