using KerbalSimpitRefactored.Common.Messages.Interfaces;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OxidizerDataMessage : IBasicResourceMessage
    {
        public float Max { get; set; }
        public float Available { get; set; }
    }
}
