using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Core.KSP.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NavballModeCommandMessage : ISimpitMessageData
    {
        // TODO: Check if any data is actually transmitted with this message
        // If there is, it seems unnecessary
    }
}
