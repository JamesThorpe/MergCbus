using System;
using System.Collections.Generic;
using System.Text;
using FcuCore.Communications;

namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Acon)]
    public class AcOnMessage : CbusMessageWithNodeNumberAndEvent
    {
        public AcOnMessage() : this(new byte[4]) { }
        public AcOnMessage(byte[] data) : base(OpCodes.Acon, data) { }
        public override string DisplayString => $"Accessory On, Node {NodeNumber}, Event {EventNumber}";
    }

    [CbusMessage(OpCodes.Acof)]
    public class AcOffMessage : CbusMessageWithNodeNumberAndEvent
    {
        public AcOffMessage() : this(new byte[4]) { }

        public AcOffMessage(byte[] data) : base(OpCodes.Acof, data) { }
        public override string DisplayString => $"Accessory Off, Node {NodeNumber}, Event {EventNumber}";
    }
}
