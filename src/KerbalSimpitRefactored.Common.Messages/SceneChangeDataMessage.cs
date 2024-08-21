using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SceneChangeDataMessage : ISimpitMessageData
    {
        public enum SceneChangeTypeEnum
        {
            Flight = 0x0,
            NotFlight = 0x1
        }

        public SceneChangeTypeEnum Type;
    }
}
