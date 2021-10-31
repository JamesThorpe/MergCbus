namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumberAndEvent : CbusMessageWithNodeNumber
    {
        protected CbusMessageWithNodeNumberAndEvent(OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 4);
        }
        
        public ushort EventNumber
        {
            get => (ushort)((Data[2] << 8) + Data[3]);
            set {
                Data[2] = (byte)(value >> 8);
                Data[3] = (byte)value;
            }
        }
    }
}