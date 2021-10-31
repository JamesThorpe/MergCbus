using System;
using System.Collections.Generic;
using System.Text;

namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Nnlrn)]
    public class SetNodeLearn : CbusMessageWithNodeNumber
    {
        public SetNodeLearn() : this(new byte[2]) { }
        public SetNodeLearn(byte[] data):base(OpCodes.Nnlrn, data) { }
    }

    [CbusMessage(OpCodes.Nnuln)]
    public class ReleaseNodeLearn : CbusMessageWithNodeNumber
    {
        public ReleaseNodeLearn() : this(new byte[2]) { }
        public ReleaseNodeLearn(byte[] data) : base(OpCodes.Nnuln, data) { }
    }

    [CbusMessage(OpCodes.Evlrn)]
    public class TeachNodeEvent : CbusMessageWithNodeNumberAndEvent
    {
        public TeachNodeEvent() : this(new byte[6]) { }
        public TeachNodeEvent(byte[]data ):base(OpCodes.Evlrn, data)
        {
            EnsureDataLength(data, 6);
        }

        public byte EventVariableIndex {
            get {
                return Data[4];
            } 
            set {
                Data[4] = value;
            }
        }

        public byte EventVariableValue {
            get {
                return Data[5];
            }
            set {
                Data[5] = value;
            }
        }
    }
}
