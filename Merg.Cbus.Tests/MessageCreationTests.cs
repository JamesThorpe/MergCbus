using FluentAssertions;
using Merg.Cbus.Communications.OpCodeMessages;
using Xunit;

namespace Merg.Cbus.Tests
{
    public class MessageCreationTests
    {
        [Theory]
        [InlineData(1, 2, ":S0000N9000010002;")]
        [InlineData(260, 260, ":S0000N9001040104;")]
        [InlineData(20, 20, ":S0000N9000140014;")]
        [InlineData(196, 196, ":S0000N9000C400C4;")]
        [InlineData(47802, 47802, ":S0000N90BABABABA;")]
        public void AconShouldGenerateTheCorrectTransportString(ushort nodeNumber, ushort eventNumber, string expectedTransportString)
        {
            var m = new AcOnMessage {
                NodeNumber = nodeNumber,
                EventNumber = eventNumber
            };

            m.TransportString.Should().Be(expectedTransportString);
        }
    }
}
