﻿namespace KerbalSimpit.Core.Constants
{
    internal static class MessageTypeIds
    {
        public static class Incoming
        {
            public const byte Synchronisation = 0;
            public const byte EchoRequest = 1;
            public const byte EchoResponse = 2;
            public const byte ConfigurationDefinition = 255;
            public const byte CloseSerialPort = 7;
            public const byte RegisterHandler = 8;
            public const byte DeregisterHandler = 9;
            public const byte CustomLog = 25;
            public const byte RequestMessage = 29;
        }

        public static class Outgoing
        {
            public const byte HandshakeMessage = 0;
            public const byte EchoResponse = 2;
            public const byte ConfigurationValue = 255;
        }
    }
}
