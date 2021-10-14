using System;
using System.Collections.Generic;
using System.Text;

namespace Merg.Cbus.Communications
{

    [AttributeUsage(AttributeTargets.Class)]
    public class CbusMessageAttribute : Attribute
    {
        public Cbus.OpCodes OpCode { get; }

        public CbusMessageAttribute(Cbus.OpCodes opCode)
        {
            OpCode = opCode;
        }
    }
}

