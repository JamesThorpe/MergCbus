using System.Threading.Tasks;
using FluentAssertions;
using Merg.Cbus.Communications;
using Merg.Cbus.Communications.OpCodeMessages;
using Moq;
using Xunit;

namespace Merg.Cbus.Tests
{
    public class MessengerTests
    {
        [Fact]
        public void CbusMessenger_ShouldRaiseCorrectParsedMessage_WhenTransportRaisesMessage()
        {
            var transport = new Mock<ITransport>();
            var cm = new CbusMessenger(transport.Object);
            CbusMessage m = null;
            cm.MessageReceived += (sender, args) => {
                m = args.Message;
            };

            transport.Raise(t => t.TransportMessage += null, new TransportMessageEventArgs() {Message = ":SB020N9101000005;" });

            m.Should().NotBeNull()
                .And.BeOfType<AcOffMessage>()
                .Which.Should().BeEquivalentTo(new {NodeNumber = 256, EventNumber = 5});
        }

        [Fact]
        public async Task CbusMessenger_ShouldSendCorrectlyFormattedMessage_WhenSendMessageCalled()
        {
            var transport = new Mock<ITransport>();
            var cm = new CbusMessenger(transport.Object);

            await cm.SendMessage(new AcOnMessage() {NodeNumber = 1, EventNumber = 2});

            transport.Verify(t => t.SendMessage(It.Is<string>(m => m == ":SAFA0N9000010002;")));
        }
    }
}
