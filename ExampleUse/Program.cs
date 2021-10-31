using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Merg.Cbus.Communications;
using Merg.Cbus.Communications.OpCodes;

namespace ExampleUse
{
    class Program
    {
        static async Task Main()
        {
            var sp = new SerialPort("COM3");
            sp.Open();

            var transport = new StreamTransport(sp.BaseStream);
            transport.Open();

            transport.TransportMessage += (s, e) => {
                //Logs complete messages, less the surrounding : and ;
                //throws away incomplete messages internally.
                Console.WriteLine(e.Message);
            };

            var messenger = new CbusMessenger(transport);

            messenger.MessageReceived += async (s, e) => {
                //e.Message is a fully strongly typed representation of the message, with a custom display string
                //falls back to just opcode + data for message types not represented in code
                Console.WriteLine("<- " + e.Message);

                if (e.Message is QueryNodesResponseMessage msg) {
                    await messenger.SendMessage(new ReadNodeParameterByIndexMessage() {
                        NodeNumber = msg.NodeNumber,
                        ParameterIndex = 0
                    });
                }

                if (e.Message is ReadNodeParameterByIndexResponseMessage param) {
                    if (param.ParameterIndex == 0) {
                        for (byte i = 1; i < param.ParameterValue; i++) {
                            await messenger.SendMessage(new ReadNodeParameterByIndexMessage() {
                                NodeNumber = param.NodeNumber,
                                ParameterIndex = i
                            });
                            await Task.Delay(20);
                        }
                    }
                }

            };

            messenger.MessageSent += (s, e) => {
                //Same as above, but is raised whenever a message is sent
                Console.WriteLine("-> " + e.Message);
            };


            await messenger.SendMessage(new QueryAllNodesMessage());

            //await SwitchPointTest(messenger);

            Console.ReadKey();
        }

        private static async Task SwitchPointTest(CbusMessenger messenger)
        {
            await messenger.SendMessage(new AcOnMessage
            {
                NodeNumber = 2,
                EventNumber = 1
            });

            await Task.Delay(3000);


            await messenger.SendMessage(new AcOffMessage
            {
                NodeNumber = 2,
                EventNumber = 1
            });
        }
    }
}
