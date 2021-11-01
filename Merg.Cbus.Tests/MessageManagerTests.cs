using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Merg.Cbus.Communications;
using Merg.Cbus.Communications.OpCodeMessages;
using Moq;
using Xunit;

namespace Merg.Cbus.Tests
{
    public class MessageManagerTests
    {
        [Fact]
        public async Task ManagerReturnsCorrectMessages_WhenWaitingForMultipleReplies()
        {
            var messenger = new Mock<ICbusMessenger>();
            messenger.Setup(m => m.SendMessage(It.IsAny<CbusMessage>())).Callback(() => {
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage()));
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage()));
            });

            var mm = new MessageManager(messenger.Object);
            var response = await mm.SendMessageWaitForReplies<QueryNodesResponseMessage>(new QueryAllNodesMessage());

            response.Count().Should().Be(2);
        }

        [Fact]
        public async Task ManagerReturnsCorrectMessages_WhenWaitingForASingleReply()
        {
            var messenger = new Mock<ICbusMessenger>();
            messenger.Setup(m => m.SendMessage(It.IsAny<CbusMessage>())).Callback(() => {
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage() {NodeNumber = 1}));
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage() {NodeNumber = 2}));
            });

            var mm = new MessageManager(messenger.Object);
            var response = await mm.SendMessageWaitForReply<QueryNodesResponseMessage>(new QueryAllNodesMessage());

            response.NodeNumber.Should().Be(1);
        }


        [Fact]
        public async Task ManagerReturnsCorrectMessages_WhenWaitingForASingleReply_WithAFilter()
        {
            var messenger = new Mock<ICbusMessenger>();
            messenger.Setup(m => m.SendMessage(It.IsAny<CbusMessage>())).Callback(() => {
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage() { NodeNumber = 1 }));
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage() { NodeNumber = 2 }));
            });

            var mm = new MessageManager(messenger.Object);
            var response = await mm.SendMessageWaitForReply<QueryNodesResponseMessage>(new QueryAllNodesMessage(), m => m.NodeNumber == 2);

            response.NodeNumber.Should().Be(2);
        }

        [Fact]
        public async Task ManagerShouldTimeout_WhenNotAllExpectedArrive()
        {
            var messenger = new Mock<ICbusMessenger>();
            messenger.Setup(m => m.SendMessage(It.IsAny<CbusMessage>())).Callback(() => {
                messenger.Raise(m => m.MessageReceived += null, new CbusMessageEventArgs(new QueryNodesResponseMessage() { NodeNumber = 1 }));
            });

            var mm = new MessageManager(messenger.Object);
            await Assert.ThrowsAsync<TimeoutException>(async () => {
                await mm.SendMessageWaitForReplies<QueryNodesResponseMessage>(new QueryAllNodesMessage(), 2);
            });
        }
    }
}
