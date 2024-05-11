﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KerbalSimpit.Core.Messages
{
    public struct RegisterHandler : ISimpitMessageData
    {
        public byte[] MessageTypeIds { get; set; }

        internal static RegisterHandler Deserialize(SimpitStream input)
        {
            return new RegisterHandler()
            {
                MessageTypeIds = input.ReadAll(out int offset, out int count).Skip(offset).Take(count).ToArray()
            };
        }
    }
}
