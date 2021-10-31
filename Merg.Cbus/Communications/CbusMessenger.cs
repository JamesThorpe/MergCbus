using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FcuCore.Communications;

namespace Merg.Cbus.Communications
{
    public class CbusMessenger : ICbusMessenger
    {
        private readonly ITransport _transport;
        public event EventHandler<CbusMessageEventArgs> MessageReceived;
        public event EventHandler<CbusMessageEventArgs> MessageSent;

        public CbusMessenger(ITransport transport)
        {
            _transport = transport;
            _transport.TransportMessage += HandleTransportMessage;
        }

        private void HandleTransportMessage(object sender, TransportMessageEventArgs e)
        {
            try {
                MessageReceived?.Invoke(this, new CbusMessageEventArgs(CbusMessage.FromTransportString(e.Message)));
            }catch(Exception ex) {
                throw;
            }
        }

        public async Task<bool> SendMessage(CbusMessage message)
        {
            //TODO: make configurable
            message.CanId = 125;
            message.MinorPriority = MinorPriority.Normal;
            message.MajorPriority = MajorPriority.Low;

            await _transport.SendMessage(message.TransportString);
            MessageSent?.Invoke(this, new CbusMessageEventArgs(message));
            return true;
        }

    }
}
