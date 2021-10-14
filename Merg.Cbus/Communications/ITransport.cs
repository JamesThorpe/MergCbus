using System;
using System.Threading.Tasks;

namespace Merg.Cbus.Communications
{
    public delegate Task BytesReceivedEvent(ReadOnlySpan<byte> bytes);
    public interface ITransport
    {
        event EventHandler<TransportException> TransportError;
        event EventHandler<TransportMessageEventArgs> TransportMessage; 
        Task SendMessage(string message);
    }
}