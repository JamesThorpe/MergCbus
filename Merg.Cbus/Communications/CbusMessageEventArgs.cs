using System;

namespace Merg.Cbus.Communications
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