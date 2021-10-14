namespace Merg.Cbus.Communications.OpCodes
{
    [CbusMessage(Cbus.OpCodes.Nvrd)]
    public class ReadNodeVariableMessage : CbusMessageWithNodeNumber
    {
        public ReadNodeVariableMessage() : base(Cbus.OpCodes.Nvrd, new byte[3]) { }

        public byte VariableIndex {
            get => Data[2];
            set => Data[2] = value;
        }

        public override string DisplayString =>
            $"Read Node Variable, Node Number: {NodeNumber}, Variable Index: {VariableIndex}";
    }

    [CbusMessage(Cbus.OpCodes.Nvans)]
    public class ReadNodeVariableAnswerMessage : CbusMessageWithNodeNumber
    {
        public ReadNodeVariableAnswerMessage():base(Cbus.OpCodes.Nvans, new byte[4]) { }

        public byte VariableIndex {
            get => Data[2];
            set => Data[2] = value;
        }
        public byte VariableValue {
            get => Data[3];
            set => Data[3] = value;
        }

        public override string DisplayString =>
            $"Read Variable Answer, Node Number: {NodeNumber}, Variable Index: {VariableIndex}, Value: {VariableValue}";
    }
}
