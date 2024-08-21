using SimpitRefactored.Common.Core;
using SimpitRefactored.Common.Core.Utilities;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct CustomActionGroupDisableCommandMessage : ISimpitMessageData
    {
        public FixedBuffer GroupIds;
    }
}
