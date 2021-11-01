using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Merg.Cbus.Communications;
using Merg.Cbus.Communications.OpCodeMessages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExampleUse
{
    class Program
    {
        static async Task Main()
        {
            /*
             //TODO: test with cbusServer
            using var ns = new TcpClient("localhost", 5550);
            using var transport = new StreamTransport(ns.GetStream());
            transport.Open();
            */
            using var sp = new SerialPort("COM3");
            sp.Open();
            

            using var transport = new StreamTransport(sp.BaseStream);
            transport.Open();

            transport.TransportMessage += (s, e) => {
                //Logs complete messages, throws away incomplete messages internally.
                Console.WriteLine("<- " + e.Message);
            };

            var messenger = new CbusMessenger(transport);

            messenger.MessageReceived += (s, e) => {
                //e.Message is a fully strongly typed representation of the message, with a custom display string
                //falls back to just opcode + data for message types not represented in code
                Console.WriteLine("<- " + e.Message);

                if (e.Message is AcOnMessage msg) {
                    Console.WriteLine($"ACON: NN: {msg.NodeNumber}, EN: {msg.EventNumber}");
                }
            };

            messenger.MessageSent += (s, e) => {
                //Same as above, but is raised whenever a message is sent
                Console.WriteLine("-> " + e.Message);
            };


            var mm = new MessageManager(messenger);
            var qn = await mm.SendMessageWaitForReplies<QueryNodesResponseMessage>(new QueryAllNodesMessage());

            foreach (var node in qn) {
                var nodeParamCount =
                    await mm.SendMessageWaitForReply<ReadNodeParameterByIndexResponseMessage>(
                        new ReadNodeParameterByIndexMessage(node.NodeNumber, 0),
                        m => m.ParameterIndex == 0);

                for (byte i = 1; i < nodeParamCount.ParameterValue; i++) {
                    var param = await mm.SendMessageWaitForReply<ReadNodeParameterByIndexResponseMessage>(
                        new ReadNodeParameterByIndexMessage(node.NodeNumber, i),
                        m => m.ParameterIndex == i);
                }
            }

            Console.ReadKey();
        }
    }
}
