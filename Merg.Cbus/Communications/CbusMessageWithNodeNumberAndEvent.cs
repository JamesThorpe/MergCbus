namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumberAndEvent : CbusMessageWithNodeNumber
    {
        protected CbusMessageWithNodeNumberAndEvent(OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 4);
        }
        
        public ushort EventNumber {
            get => GetUShort(2, 3);
            set => SetUShort(value, 2, 3);
        }
    }
}