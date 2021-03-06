namespace Merg.Cbus.Communications.OpCodeMessages
{
    //send:
    //:SBFA0N0D;

    //receive:
    //:SB060NB60102A5080D - acc4
    //:SB020NB60100A5050F - ace8c
    //:SB040NB60101A5050F - ace8c

    [CbusMessage(OpCodes.Qnn)]
    public class QueryAllNodesMessage : CbusMessage
    {
        public QueryAllNodesMessage() : this(new byte[0]) { }
        public QueryAllNodesMessage(byte[] data) : base(OpCodes.Qnn, data) { }

        public override string DisplayString => "Query all nodes";
    }


    [CbusMessage(OpCodes.Pnn)]
    public class QueryNodesResponseMessage : CbusMessageWithNodeNumber
    {
        public QueryNodesResponseMessage() : base(OpCodes.Pnn, new byte[5]) { }

        public QueryNodesResponseMessage(byte[] data) : base(OpCodes.Pnn, data) { }

        public byte ManufacturerId => Data[2];
        public byte ModuleId => Data[3];

        public bool IsConsumerNode => GetBit(4, 0b0001);
        public bool IsProducerNode => GetBit(4, 0b0010); 
        public bool InFlimMode => GetBit(4, 0b0100);
        public bool SupportsBootloader => GetBit(4, 0b1000);

        public override string DisplayString => $"Query Node Response, Manufacturer Id {ManufacturerId}, Module Id {ModuleId}, Consumer: {IsConsumerNode}, Producer: {IsProducerNode}, Flim: {InFlimMode}, SupportsBootloader: {SupportsBootloader}";
    }
}
