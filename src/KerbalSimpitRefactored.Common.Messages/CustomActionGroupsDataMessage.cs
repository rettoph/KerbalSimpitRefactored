using SimpitRefactored.Common.Core;
using System.Runtime.InteropServices;

namespace KerbalSimpitRefactored.Common.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct CustomActionGroupsDataMessage : ISimpitMessageData
    {
        public const int Length = 32;
        public fixed byte Status[CustomActionGroupsDataMessage.Length];

        public bool Equals(CustomActionGroupsDataMessage obj)
        {
            for (int i = 0; i < CustomActionGroupsDataMessage.Length; i++)
            {
                if (this.Status[i] != obj.Status[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
