namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Ason)]
    public class AsOnMessage : CbusMessageWithNodeNumberAndDeviceNumber
    {
        public AsOnMessage() : base(OpCodes.Ason, new byte[4]) { }
        public override string DisplayString => $"Accessory On (Short), DeviceNumber: {DeviceNumber}, Node: {NodeNumber}";
    }

    [CbusMessage(OpCodes.Asof)]
    public class AsOffMessage : CbusMessageWithNodeNumberAndDeviceNumber
    {
        public AsOffMessage() : base(OpCodes.Asof, new byte[4]) { }
        public override string DisplayString => $"Accessory Off (Short), DeviceNumber: {DeviceNumber}, Node: {NodeNumber}";
    }
}