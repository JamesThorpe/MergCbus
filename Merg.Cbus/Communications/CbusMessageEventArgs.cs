using Merg.Cbus.Communications;
using System;

namespace FcuCore.Communications
{
    public class CbusMessageEventArgs : EventArgs
    {
        public CbusMessage Message { get; }

        public CbusMessageEventArgs(CbusMessage message)
        {
            Message = message;
        }
    }
}