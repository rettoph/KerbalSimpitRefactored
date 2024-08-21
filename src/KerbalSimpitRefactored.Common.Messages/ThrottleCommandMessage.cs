﻿using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ThrottleCommandMessage : ISimpitMessageData
    {
        public short Value { get; set; }
    }
}