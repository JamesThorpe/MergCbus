namespace Merg.Cbus.Communications.OpCodes
{
    [CbusMessage(Cbus.OpCodes.Rqnpn)]
    public class ReadNodeParameterByIndexMessage: CbusMessageWithNodeNumber
    {
        public ReadNodeParameterByIndexMessage() : base(Cbus.OpCodes.Rqnpn, new byte[3]) { }

        public byte ParameterIndex
        {
            get => Data[2];
            set => Data[2] = value;
        }

        public override string DisplayString => $"Read Node Parameter, Node Number: {NodeNumber}, Param Index: {ParameterIndex}";
    }


    [CbusMessage(Cbus.OpCodes.Paran)]
    public class ReadNodeParameterByIndexResponseMessage : CbusMessageWithNodeNumber
    {
        public ReadNodeParameterByIndexResponseMessage() : base(Cbus.OpCodes.Paran, new byte[4]) { }

        public ReadNodeParameterByIndexResponseMessage(byte[] data) :base(Cbus.OpCodes.Paran, data)
        {

        }

        public byte ParameterIndex
        {
            get => Data[2];
            set => Data[2] = value;
        }

        public byte ParameterValue
        {
            get => Data[3];
            set => Data[3] = value;
        }

        public override string DisplayString =>
            $"Read Parameter Response, Node Number: {NodeNumber}, Param Index: {ParameterIndex}, Param Value: {ParameterValue}";
    }

}
