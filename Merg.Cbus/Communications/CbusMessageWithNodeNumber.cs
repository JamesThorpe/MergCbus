
using System;

namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumber : CbusMessage
    {
        protected CbusMessageWithNodeNumber(OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 2);
        }

        public ushort NodeNumber {
            get => (ushort) ((Data[0] << 8) + Data[1]);
            set {
                Data[0] = (byte)(value >> 8);
                Data[1] = (byte) value;
            }
        }
    }
}