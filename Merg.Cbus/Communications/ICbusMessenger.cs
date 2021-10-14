using Merg.Cbus.Communications;
using System;
using System.Threading.Tasks;

namespace FcuCore.Communications
{
    public interface ICbusMessenger
    {
        event EventHandler<CbusMessageEventArgs> MessageReceived;
        event EventHandler<CbusMessageEventArgs> MessageSent;

        Task<bool> SendMessage(CbusMessage message);
    }
}