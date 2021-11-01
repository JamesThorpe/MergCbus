using FluentAssertions;
using Merg.Cbus.Communications;
using System;
using System.IO;
using System.Text;
using System.Threading;
using Xunit;

namespace Merg.Cbus.Tests
{
    public class StreamTransportTests
    {
        [Theory]
        [InlineData(":somemsg;", ":somemsg;")]
        [InlineData(":;", ":;")]
        [InlineData(":s;", ":s;")]
        public void StreamTransport_RaisesSingleEvent_ForEachSingleMessage(string input, string output)
        {
            var m = new MemoryStream(Encoding.ASCII.GetBytes(input));
            var mre = new ManualResetEvent(false);
            m.Seek(0, SeekOrigin.Begin);
            var s = new StreamTransport(m);

            string msg = null;
            s.TransportMessage += (o, e) => {
                msg = e.Message;
                mre.Set();
            };

            s.Open();

            mre.WaitOne(TimeSpan.FromSeconds(2));
            msg.Should().Be(output);
        }


        [Theory]
        [InlineData("abc:somemsg;", ":somemsg;")]
        [InlineData(":somemsg;123", ":somemsg;")]
        [InlineData("abc:somemsg;123", ":somemsg;")]
        [InlineData("a;bc:somemsg;123", ":somemsg;")]
        public void StreamTransport_RaisesSingleEvent_AndIgnoresPartialMessages(string input, string output)
        {
            var m = new MemoryStream(Encoding.ASCII.GetBytes(input));
            var mre = new ManualResetEvent(false);
            m.Seek(0, SeekOrigin.Begin);
            var s = new StreamTransport(m);

            string msg = null;
            s.TransportMessage += (o, e) => {
                msg = e.Message;
                mre.Set();
            };

            s.Open();

            mre.WaitOne(TimeSpan.FromSeconds(2));
            msg.Should().Be(output);
        }
    }
}
