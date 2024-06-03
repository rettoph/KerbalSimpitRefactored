using System;

namespace KerbalSimpit.Core.Messages
{
    public unsafe struct ConfigurationValue : ISimpitMessageData
    {
        public byte Id;
        public fixed byte Value[4];

        public void SetValue<T>(T value)
            where T : unmanaged
        {
            int size = Math.Min(sizeof(T), 4);
            byte* bytes = (byte*)&value;

            for (int i = 0; i < size; i++)
            {
                this.Value[i] = bytes[i];
            }
        }
    }
}
