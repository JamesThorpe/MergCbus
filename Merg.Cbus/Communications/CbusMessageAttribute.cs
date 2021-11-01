using System;

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

