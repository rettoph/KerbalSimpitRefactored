using KerbalSimpit.Core.Messages;
using KerbalSimpit.Core.Peers;
using System;

namespace KerbalSimpit.Core
{
    public class PeerConfiguration
    {
        public readonly SimpitPeer Peer;
        public readonly ConfigurationDefinition Definition;
        public object CurrentValue { get; private set; }

        public PeerConfiguration(SimpitPeer peer, ConfigurationDefinition definition)
        {
            this.Peer = peer;
            this.Definition = definition;
            this.CurrentValue = this.Definition.GetInitialValue();
        }

        public void Set<T>(T value)
            where T : unmanaged
        {
            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.Byte && typeof(T) == typeof(Byte))
            {
                this.SendValue(value);
            }

            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.Int16 && typeof(T) == typeof(Int16))
            {
                this.SendValue(value);
            }

            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.UInt16 && typeof(T) == typeof(UInt16))
            {
                this.SendValue(value);
            }

            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.Single && typeof(T) == typeof(Single))
            {
                this.SendValue(value);
            }

            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.Int32 && typeof(T) == typeof(Int32))
            {
                this.SendValue(value);
            }

            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.UInt32 && typeof(T) == typeof(UInt32))
            {
                this.SendValue(value);
            }

            if (this.Definition.Type == Enums.ConfigurationValueTypeEnum.Boolean && typeof(T) == typeof(Boolean))
            {
                this.SendValue(value);
            }

            throw new NotImplementedException();
        }

        private void SendValue<T>(T value)
            where T : unmanaged
        {
            ConfigurationValue message = new ConfigurationValue();
            message.SetValue(value);

            this.Peer.EnqueueOutgoing(message);
        }
    }
}