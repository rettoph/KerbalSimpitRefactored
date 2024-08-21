using SimpitRefactored.Common.Core.Utilities;
using SimpitRefactored.Core;
using SimpitRefactored.Core.Messages;
using SimpitRefactored.Core.Peers;
using SimpitRefactored.Unity.Common;

namespace KerbalSimpitRefactored.Unity.KSP1.Providers
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class CoreProvider : SimpitBehaviour,
        ISimpitMessageSubscriber<EchoRequest>,
        ISimpitMessageSubscriber<EchoResponse>,
        ISimpitMessageSubscriber<CustomLog>
    {
        public override void Start()
        {
            base.Start();

            DontDestroyOnLoad(this); // Make this provider persistent
        }

        void ISimpitMessageSubscriber<EchoRequest>.Process(SimpitPeer peer, ISimpitMessage<EchoRequest> message)
        {
            this.logger.LogVerbose("Echo request on peer {0}. Replying.", peer);
            peer.EnqueueOutgoing(new EchoResponse()
            {
                Data = message.Data.Data
            });
        }

        void ISimpitMessageSubscriber<EchoResponse>.Process(SimpitPeer peer, ISimpitMessage<EchoResponse> message)
        {
            this.logger.LogVerbose("Echo reply received on port {0}", peer);
        }

        void ISimpitMessageSubscriber<CustomLog>.Process(SimpitPeer peer, ISimpitMessage<CustomLog> message)
        {
            if (message.Data.Flags.HasFlag(CustomLog.FlagsEnum.Verbose))
            {
                this.logger.LogVerbose(message.Data.Value);
            }

            if (message.Data.Flags.HasFlag(CustomLog.FlagsEnum.PrintToScreen))
            {
                ScreenMessages.PostScreenMessage(message.Data.Value);
            }
        }
    }
}
