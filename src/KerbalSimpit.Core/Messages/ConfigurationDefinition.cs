using KerbalSimpit.Core.Enums;
using KerbalSimpit.Core.Utilities;
using System;
using System.Runtime.InteropServices;

namespace KerbalSimpit.Core.Messages
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConfigurationDefinition : ISimpitMessageData
    {
        public byte Id;
        public FixedString Name;
        public ConfigurationValueTypeEnum Type;
        public unsafe fixed byte InitialValue[4];

        public unsafe object GetInitialValue()
        {
            fixed (byte* bytes = &this.InitialValue[0])
            {
                switch (this.Type)
                {
                    case ConfigurationValueTypeEnum.Byte:
                        return bytes[0];
                    case ConfigurationValueTypeEnum.Int16:
                        return ((short*)bytes)[0];
                    case ConfigurationValueTypeEnum.UInt16:
                        return ((short*)bytes)[0];
                    case ConfigurationValueTypeEnum.Single:
                        return ((ushort*)bytes)[0];
                    case ConfigurationValueTypeEnum.Int32:
                        return ((int*)bytes)[0];
                    case ConfigurationValueTypeEnum.UInt32:
                        return ((uint*)bytes)[0];
                    case ConfigurationValueTypeEnum.Boolean:
                        return ((bool*)bytes)[0];
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
