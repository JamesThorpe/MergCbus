using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Merg.Cbus.Communications;
using Merg.Cbus.Communications.OpCodes;

namespace ExampleUse
{
    class Program
    {
        static void Main()
        {
            var sp = new SerialPort("COM1");

            var transport = new StreamTransport(sp.BaseStream);
            transport.TransportMessage += (s, e) => {
                //Logs complete messages, less the surrounding : and ;
                //throws away incomplete messages internally.
                Console.WriteLine(e.Message);
            };

            var messenger = new CbusMessenger(transport);

            messenger.MessageReceived += (s, e) => {
                //e.Message is a fully strongly typed representation of the message, with a custom display string
                //falls back to just opcode + data for message types not represented in code
                Console.WriteLine(e.Message);
            };

            messenger.MessageSent += (s, e) => {
                //Same as above, but is raised whenever a message is sent
                Console.WriteLine(e.Message);
            };

            /*
             Incomplete as yet, but sending messages will be along these lines
            
            await messenger.SendMessage(new AcOnMessage {
                NodeNumber = 20,
                EventNumber = 3
            });
            */

            Console.ReadKey();
        }
    }
}
