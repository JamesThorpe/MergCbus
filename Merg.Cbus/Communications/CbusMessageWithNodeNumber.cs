
namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumber : CbusMessage
    {
        protected CbusMessageWithNodeNumber(OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 2);
        }

        public ushort NodeNumber {
            get => GetUShort(0, 1);
            set => SetUShort(value, 0, 1);
        }
    }
}