using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Merg.Cbus.Communications
{
    public class CbusMessenger : ICbusMessenger
    {
        private readonly ITransport _transport;
        private readonly ILogger<CbusMessenger> _logger;
        public event EventHandler<CbusMessageEventArgs> MessageReceived;
        public event EventHandler<CbusMessageEventArgs> MessageSent;

        public CbusMessenger(ITransport transport, ILogger<CbusMessenger> logger = null)
        {
            _transport = transport;
            _logger = logger;
            _transport.TransportMessage += HandleTransportMessage;
        }

        private void HandleTransportMessage(object sender, TransportMessageEventArgs e)
        {
            try {
                var msg = CbusMessage.FromTransportString(e.Message);
                _logger?.LogTrace("Parsed received Message: {0}", msg);
                MessageReceived?.Invoke(this, new CbusMessageEventArgs(msg));
            } catch (Exception ex) {
                _logger?.LogError(ex, @"Error parsing message ""{0}""", e.Message);
                //TODO: wrap exception?
                throw;
            }
        }

        public async Task<bool> SendMessage(CbusMessage message)
        {
            //TODO: make configurable
            message.CanId = 125;
            message.MinorPriority = MinorPriority.Normal;
            message.MajorPriority = MajorPriority.Low;

            _logger?.LogTrace("Sending message: {0}", message);
            await _transport.SendMessage(message.TransportString);
            MessageSent?.Invoke(this, new CbusMessageEventArgs(message));
            return true;
        }

    }
}
