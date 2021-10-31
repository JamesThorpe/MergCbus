using System;
using System.Collections.Generic;
using System.Text;

namespace Merg.Cbus.Communications
{

    [AttributeUsage(AttributeTargets.Class)]
    public class CbusMessageAttribute : Attribute
    {
        public OpCodes OpCode { get; }

        public CbusMessageAttribute(OpCodes opCode)
        {
            OpCode = opCode;
        }
    }
}

