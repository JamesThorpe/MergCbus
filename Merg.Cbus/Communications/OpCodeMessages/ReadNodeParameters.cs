namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Rqnpn)]
    public class ReadNodeParameterByIndexMessage : CbusMessageWithNodeNumber
    {
        public ReadNodeParameterByIndexMessage() : this(new byte[3]) { }
        public ReadNodeParameterByIndexMessage(byte[] data) : base(OpCodes.Rqnpn, data) {
            EnsureDataLength(data, 3);
        }

        public byte ParameterIndex {
            get => Data[2];
            set => Data[2] = value;
        }

        public override string DisplayString => $"Read Node Parameter, Node Number: {NodeNumber}, Param Index: {ParameterIndex}";
    }


    [CbusMessage(OpCodes.Paran)]
    public class ReadNodeParameterByIndexResponseMessage : CbusMessageWithNodeNumber
    {
        public ReadNodeParameterByIndexResponseMessage() : this(new byte[4]) { }

        public ReadNodeParameterByIndexResponseMessage(byte[] data) : base(OpCodes.Paran, data)
        {

        }

        public byte ParameterIndex {
            get => Data[2];
            set => Data[2] = value;
        }

        public byte ParameterValue {
            get => Data[3];
            set => Data[3] = value;
        }

        public override string DisplayString =>
            $"Read Parameter Response, Node Number: {NodeNumber}, Param Index: {ParameterIndex}, Param Value: {ParameterValue}";
    }

}
