using System;
using System.Threading.Tasks;

namespace Merg.Cbus.Communications
{
    public delegate Task BytesReceivedEvent(ReadOnlySpan<byte> bytes);
    public interface ITransport
    {
        /// <summary>
        /// Fires when an error occurs at the transport layer. Incomplete messages are viewable here.
        /// </summary>
        event EventHandler<TransportException> TransportError;
        /// <summary>
        /// Fires when a message has been received.
        /// </summary>
        /// <remarks>
        /// Only complete messages are raised via this event.  Incomplete messages are dealt with and removed internally.
        /// </remarks>
        event EventHandler<TransportMessageEventArgs> TransportMessage; 
        /// <summary>
        /// Used to send a new message on the transport layer.
        /// </summary>
        /// <param name="message">The message to send, in GridConnect format.</param>
        /// <returns>A task that represents the asynchronous send operation.</returns>
        Task SendMessage(string message);
    }
}