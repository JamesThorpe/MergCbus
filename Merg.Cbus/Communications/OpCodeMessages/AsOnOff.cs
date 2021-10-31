namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Ason)]
    public class AsOnMessage : CbusMessageWithNodeNumberAndDeviceNumber
    {
        public AsOnMessage() : this(new byte[4]) { }
        public AsOnMessage(byte[] data) : base(OpCodes.Ason, data) { }
        public override string DisplayString => $"Accessory On (Short), DeviceNumber: {DeviceNumber}, Node: {NodeNumber}";
    }

    [CbusMessage(OpCodes.Asof)]
    public class AsOffMessage : CbusMessageWithNodeNumberAndDeviceNumber
    {
        public AsOffMessage() : this(new byte[4]) { }
        public AsOffMessage(byte[] data) : base(OpCodes.Asof, data) { }
        public override string DisplayString => $"Accessory Off (Short), DeviceNumber: {DeviceNumber}, Node: {NodeNumber}";
    }
}