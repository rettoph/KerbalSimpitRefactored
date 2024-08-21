﻿using SimpitRefactored.Common.Core;

namespace KerbalSimpitRefactored.Common.Messages.Interfaces
{
    public interface IBasicResourceMessage : ISimpitMessageData
    {
        float Max { get; set; }
        float Available { get; set; }
    }
}