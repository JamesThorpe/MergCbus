using System;
using System.Collections.Generic;
using System.Text;
using FcuCore.Communications;

namespace Merg.Cbus.Communications.OpCodeMessages
{
    [CbusMessage(OpCodes.Acon)]
    public class AcOnMessage : CbusMessageWithNodeNumberAndEvent
    {
        public AcOnMessage() : base(OpCodes.Acon, new byte[4]) { }
        public override string DisplayString => $"Accessory On, Node {NodeNumber}, Event {EventNumber}";
    }

    [CbusMessage(OpCodes.Acof)]
    public class AcOffMessage : CbusMessageWithNodeNumberAndEvent
    {
        public AcOffMessage() : base(OpCodes.Acof, new byte[4]) { }

        public AcOffMessage(byte[] data) : base(OpCodes.Acof, data) { }
        public override string DisplayString => $"Accessory Off, Node {NodeNumber}, Event {EventNumber}";
    }
}
