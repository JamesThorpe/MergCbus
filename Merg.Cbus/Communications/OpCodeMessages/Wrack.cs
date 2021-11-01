namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Wrack)]
    public class Wrack:CbusMessageWithNodeNumber
    {
        public Wrack() : this(new byte[2]) { }
        public Wrack(byte[] data) : base(OpCodes.Wrack, data) { }
    }
}
