namespace Merg.Cbus.Communications {
    public abstract class CbusMessageWithNodeNumberAndEvent : CbusMessageWithNodeNumber
    {
        protected CbusMessageWithNodeNumberAndEvent(Cbus.OpCodes opCode, byte[] data) : base(opCode, data)
        {
            EnsureDataLength(data, 4);
        }
        
        public ushort EventNumber => (ushort)((Data[2] << 8) + Data[3]);
    }
}