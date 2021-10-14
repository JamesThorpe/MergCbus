using System;
using System.Collections.Generic;
using System.Text;
using FcuCore.Communications;

namespace Merg.Cbus.Communications.OpCodes
{
    [CbusMessage(Cbus.OpCodes.Acon)]
    public class AcOnMessage : CbusMessageWithNodeNumberAndEvent
    {
        public AcOnMessage() : base(Cbus.OpCodes.Acon, new byte[4]) { }
        public override string DisplayString => $"Accessory On, Node {NodeNumber}, Event {EventNumber}";
    }

    [CbusMessage(Cbus.OpCodes.Acof)]
    public class AcOffMessage : CbusMessageWithNodeNumberAndEvent
    {
        public AcOffMessage() : base(Cbus.OpCodes.Acof, new byte[4]) { }

        public AcOffMessage(byte[] data) : base(Cbus.OpCodes.Acof, data) { }
        public override string DisplayString => $"Accessory Off, Node {NodeNumber}, Event {EventNumber}";
    }
}
