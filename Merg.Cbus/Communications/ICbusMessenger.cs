using System;
using System.Threading.Tasks;

namespace Merg.Cbus.Communications
{
    public interface ICbusMessenger
    {
        event EventHandler<CbusMessageEventArgs> MessageReceived;
        event EventHandler<CbusMessageEventArgs> MessageSent;

        Task<bool> SendMessage(CbusMessage message);
    }
}