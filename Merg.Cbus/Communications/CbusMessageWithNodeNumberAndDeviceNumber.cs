namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumberAndDeviceNumber : CbusMessageWithNodeNumber
    {
        protected CbusMessageWithNodeNumberAndDeviceNumber(OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 4);
        }

        public ushort DeviceNumber {
            get => GetUShort(2, 3);
            set => SetUShort(value, 2, 3);
        }
    }
}