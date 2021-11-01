using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Merg.Cbus.Extensions;
using Microsoft.Extensions.Logging;

namespace Merg.Cbus.Communications
{
    public sealed class StreamTransport : ITransport, IDisposable
    {

        public event EventHandler<TransportException> TransportError;
        public event EventHandler<TransportMessageEventArgs> TransportMessage;

        private CancellationTokenSource _cts;
        private readonly Stream _stream;
        private readonly ILogger<StreamTransport> _logger;

        public StreamTransport(Stream stream, ILogger<StreamTransport> logger = null)
        {
            _stream = stream;
            _logger = logger;
        }
        
        /// <summary>
        /// Used to initialise communications on the provided Stream.
        /// </summary>
        public void Open()
        {
            _cts = new CancellationTokenSource();
            var pipe = new Pipe();
            ReadPipe(pipe.Reader).CbusFireAndForget();
            Listen(pipe.Writer).CbusFireAndForget();
        }

        public void Close()
        {
            _cts.Cancel();
        }

        private Task Listen(PipeWriter writer)
        {
            return Task.Run(async () => {
                const int minBufferSize = 64;
                while (!_cts.IsCancellationRequested) {
                    var memory = writer.GetMemory(minBufferSize);
                    try {
                        var read = await _stream.ReadAsync(memory, _cts.Token);
                        if (read == 0) {
                            //todo?
                        }

                        writer.Advance(read);
                        await writer.FlushAsync(_cts.Token);
                    } catch (TaskCanceledException) {
                        //ok
                    } catch (Exception e) {
                        TransportError?.Invoke(this, new TransportException("Error receiving bytes", e));
                    }
                }
            });
        }

        private Task ReadPipe(PipeReader reader)
        {
            return Task.Run(async () => {
                while (!_cts.IsCancellationRequested) {
                    var result = await reader.ReadAsync(_cts.Token);
                    var buffer = result.Buffer;
                    SequencePosition? endPosition;
                    do {
                        endPosition = buffer.PositionOf((byte) ';');
                        if (endPosition != null) {
                            endPosition = buffer.GetPosition(1, endPosition.Value);
                            ProcessMessage(buffer.Slice(0, endPosition.Value));
                            buffer = buffer.Slice(endPosition.Value);
                        }

                    } while (endPosition != null);

                    reader.AdvanceTo(buffer.Start, buffer.End);
                }
            });
        }

        private void ProcessMessage(ReadOnlySequence<byte> readOnlySequence)
        {
            var startPosition = readOnlySequence.PositionOf((byte) ':');
            if (startPosition != null) {
                var msg = GetMessageString(readOnlySequence.Slice(startPosition.Value, readOnlySequence.End));
                _logger?.LogTrace("Message received {0}", msg);
                TransportMessage?.Invoke(this, new TransportMessageEventArgs { Message = msg });
            } else {
                //TODO: is there a way to sensibly combine the two string generations to only do it once, but also only when needed
                _logger?.LogWarning("Partial message received: {0}", GetMessageString(readOnlySequence));
                TransportError?.Invoke(this, new TransportException($"Partial message received: {GetMessageString(readOnlySequence)}"));
            }
        }

        string GetMessageString(ReadOnlySequence<byte> buffer)
        {
            if (buffer.IsSingleSegment) {
                return Encoding.ASCII.GetString(buffer.First.Span);
            }

            return string.Create((int)buffer.Length, buffer, (span, sequence) =>
            {
                foreach (var segment in sequence) {
                    Encoding.ASCII.GetChars(segment.Span, span);
                    span = span[segment.Length..];
                }
            });
        }

        public async Task SendMessage(string message)
        {
            try {
                _logger?.LogTrace("Sending message: {0}", message);
                await _stream.WriteAsync(Encoding.ASCII.GetBytes(message), _cts.Token);
                await _stream.FlushAsync();
            } catch (TaskCanceledException) {
                //ok
            } catch (Exception e) {
                _logger?.LogError("Error sending message", e);
                TransportError?.Invoke(this, new TransportException("Error sending message", e));
            }
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _cts?.Dispose();
        }
    }
}
