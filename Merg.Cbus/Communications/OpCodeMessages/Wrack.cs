using System;
using System.Collections.Generic;
using System.Text;

namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Wrack)]
    public class Wrack:CbusMessageWithNodeNumber
    {
        public Wrack() : this(new byte[2]) { }
        public Wrack(byte[] data) : base(OpCodes.Wrack, data) { }
    }
}
