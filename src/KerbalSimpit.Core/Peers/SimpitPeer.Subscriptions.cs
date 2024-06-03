using KerbalSimpit.Core.Enums;
using KerbalSimpit.Core.Messages;
using KerbalSimpit.Core.Utilities;
using System.Linq;

namespace KerbalSimpit.Core.Peers
{
    public partial class SimpitPeer
    {
        internal void ProcessSYN(Synchronisation message)
        {
            // Remove all messages not yet sent to make sure the next message sent is an SYNACK
            _outgoing.Clear();
            _outgoing.Clear();

            this.Status = ConnectionStatusEnum.HANDSHAKE;
            this.EnqueueOutgoing(new Handshake()
            {
                Payload = 0x37, // TODO: Figure out what this is.
                HandShakeType = (byte)Synchronisation.SynchronisationMessageTypeEnum.SYNACK
            });
        }

        internal void ProcessACK(Synchronisation message)
        {
            this.Status = ConnectionStatusEnum.CONNECTED;
        }

        internal void Process(RegisterHandler message)
        {
            foreach (byte messageTypeId in message.MessageTypeIds)
            {
                if (messageTypeId == default)
                { // Ignore default requests
                    continue;
                }

                if (_simpit.Messages.TryGetOutgoingType(messageTypeId, out SimpitMessageType type) == false)
                {
                    this.logger.LogWarning("{0}::{1} - Unrecognized registration request from {2}, {3}", nameof(SimpitPeer), nameof(Process), this, messageTypeId);
                    continue;
                }

                try
                {
                    this.logger.LogVerbose("{0}::{1} - {2} subscribing to message type {3}", nameof(Simpit), nameof(Process), this, type);
                    _outgoingSubscriptions.TryAdd(type, default);
                    _simpit.GetOutgoingData(type).AddSubscriber(this);
                    this.OnOutgoingSubscribed?.Invoke(this, type);
                }
                catch
                {
                    this.logger.LogWarning("{0}::{1} - {2} already subscribed to message type {3}", nameof(Simpit), nameof(Process), this, type);
                }
            }
        }

        internal void Process(DeregisterHandler message)
        {
            foreach (byte messageTypeId in message.MessageTypeIds)
            {
                if (messageTypeId == default)
                { // Ignore default requests
                    continue;
                }

                if (_simpit.Messages.TryGetOutgoingType(messageTypeId, out SimpitMessageType type) == false)
                {
                    this.logger.LogWarning("{0}::{1} - Unrecognized deregistration request from {2}, {3}", nameof(Simpit), nameof(ISimpitMessageSubscriber<RegisterHandler>.Process), this, messageTypeId);
                    continue;
                }

                try
                {
                    this.logger.LogVerbose("{0}::{1} - Peer {2} unsubscribing from message type {3}", nameof(Simpit), nameof(ISimpitMessageSubscriber<RegisterHandler>.Process), this, type);
                    _outgoingSubscriptions.TryRemove(type, out _);
                    _simpit.GetOutgoingData(type).RemoveSubscriber(this);
                    this.OnOutgoingUnsubscribed?.Invoke(this, type);
                }
                catch
                {
                    this.logger.LogWarning("{0}::{1} - Peer {2} already unsubscribed from message type {3}", nameof(Simpit), nameof(ISimpitMessageSubscriber<RegisterHandler>.Process), this, type);
                }
            }
        }

        internal void Process(ConfigurationDefinition message)
        {
            PeerConfiguration existing = _configurations.FirstOrDefault(x => x.Definition.Id == message.Id);
            if (existing != null)
            {
                this.logger.LogError("{0}::{1} - Incoming configuration definition id {2} already belongs to {3}", nameof(Simpit), nameof(ISimpitMessageSubscriber<ConfigurationDefinition>.Process), message.Id, existing.Definition.Name);
                return;
            }

            this.logger.LogDebug("{0}::{1} - Incoming configuration definition {2}:{3} of type {4} with initial value '{5}'", nameof(Simpit), nameof(ISimpitMessageSubscriber<ConfigurationDefinition>.Process), message.Id, message.Name, message.Type, message.GetInitialValue());

            PeerConfiguration configuration = new PeerConfiguration(this, message);
            _configurations.Add(configuration);
            this.OnConfigurationAdded?.Invoke(this, configuration);
        }
    }
}
