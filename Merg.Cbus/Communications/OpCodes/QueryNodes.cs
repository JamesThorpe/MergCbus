namespace Merg.Cbus.Communications.OpCodes
{
    //send:
    //:SBFA0N0D;

    //receive:
    //:SB060NB60102A5080D - acc4
    //:SB020NB60100A5050F - ace8c
    //:SB040NB60101A5050F - ace8c

    [CbusMessage(Cbus.OpCodes.Qnn)]
    public class QueryAllNodesMessage:CbusMessage
    {
        public QueryAllNodesMessage():base(Cbus.OpCodes.Qnn,new byte[0]) { }

        public override string DisplayString => "Query all nodes";
    }


    [CbusMessage(Cbus.OpCodes.Pnn)]
    public class QueryNodesResponseMessage : CbusMessageWithNodeNumber
    {
        public QueryNodesResponseMessage() : base(Cbus.OpCodes.Pnn, new byte[5]) { }

        public QueryNodesResponseMessage(byte[] data) : base(Cbus.OpCodes.Pnn, data) { }

        public byte ManufacturerId => Data[2];
        public byte ModuleId => Data[3];

        public bool IsConsumerNode => (Data[4] & 0b0001) == 0b0001;
        public bool IsProducerNode => (Data[4] & 0b0010) == 0b0010;
        public bool InFlimMode => (Data[4] & 0b0100) == 0b0100;
        public bool SupportsBootloader => (Data[4] & 0b1000) == 0b1000;

        public override string DisplayString => $"Query Node Response, Manufacturer Id {ManufacturerId}, Module Id {ModuleId}";
    }
}
