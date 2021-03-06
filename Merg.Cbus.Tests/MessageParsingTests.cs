using FluentAssertions;
using Merg.Cbus.Communications;
using Merg.Cbus.Communications.OpCodeMessages;
using Xunit;

namespace Merg.Cbus.Tests
{

    public class MessageParsingTests
    {
        [Fact]
        public void CbusMessage_ParsesBasicMessage()
        {
            var m = CbusMessage.FromTransportString(":SB020N9101000005;");

            m.Should().BeOfType<AcOffMessage>();
            var msg = (AcOffMessage) m;
            msg.NodeNumber.Should().Be(256);
            msg.EventNumber.Should().Be(5);
        }
    }
}
