using System;

namespace Merg.Cbus.Communications
{
    public class TransportMessageEventArgs:EventArgs
    {
        public string Message { get; set; }
    }
}