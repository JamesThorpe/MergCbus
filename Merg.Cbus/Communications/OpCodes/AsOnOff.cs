namespace Merg.Cbus.Communications.OpCodes
{
    [CbusMessage(Cbus.OpCodes.Ason)]
    public class AsOnMessage : CbusMessageWithNodeNumberAndDeviceNumber
    {
        public AsOnMessage() : base(Cbus.OpCodes.Ason, new byte[4]) { }
        public override string DisplayString => $"Accessory On (Short), DeviceNumber: {DeviceNumber}, Node: {NodeNumber}";
    }

    [CbusMessage(Cbus.OpCodes.Asof)]
    public class AsOffMessage : CbusMessageWithNodeNumberAndDeviceNumber
    {
        public AsOffMessage() : base(Cbus.OpCodes.Asof, new byte[4]) { }
        public override string DisplayString => $"Accessory Off (Short), DeviceNumber: {DeviceNumber}, Node: {NodeNumber}";
    }
}