﻿using KerbalSimpit.Common.Core;
using KerbalSimpit.Common.Core.Utilities;

namespace KerbalSimpit.Core.Messages
{
    public struct Synchronisation : ISimpitMessageData
    {
        public enum SynchronisationMessageTypeEnum : byte
        {
            SYN = 0x0,
            SYNACK = 0x1,
            ACK = 0x2
        }

        public SynchronisationMessageTypeEnum Type { get; set; }
        public FixedString Version { get; set; }
    }
}
