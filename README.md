# MERG CBUS Communications

This library is designed to provide message parsing and general communications support for the [MERG CBUS](https://merg.org.uk/resources/cbus) Layout Control Bus.

Various layers are present, from the underlying transport through to a high level message handling mechanism.

#### Design Goals

The underlying design principles for the library are to use modern .NET techniques for dealing with asynchronous code, and also to remain as highly performant as possible, reducing allocations for parsed messages to the bare minimum, while handling all aspects of communicating with a CBUS system in a robust manner.

#### Transport

At the lowest level, to begin communicating with CBUS over a `Stream`, such as a COM port:

```C#
using var sp = new SerialPort("COM3");
sp.Open();

using var transport = new StreamTransport(sp.BaseStream);
transport.Open();

transport.TransportMessage += (s, e) => {
    //Logs complete messages, throws away incomplete messages internally.
    Console.WriteLine(e.Message);
};
```

Alternatively, (though currently untested), the following code would allow network communications with a shared [mergCbusServer](https://github.com/phillipsnj/mergCbusServer):

```C#
using var ns = new TcpClient("localhost", 5550);
using var transport = new StreamTransport(ns.GetStream());
transport.Open();
```

#### Messages

The next level up provides message parsing functionality, to turn the messages retrieved from a transport provider (currently only `StreamTransport` into strongly typed messages):

```C#
// pass in the previously created transport object
var messenger = new CbusMessenger(transport);

messenger.MessageReceived += (s, e) => {
    //e.Message is a fully strongly typed representation of the message, with a custom display string
    //falls back to just opcode + data for message types not yet represented in code
    Console.WriteLine("<- " + e.Message);

    // eg you could do:
    if (e.Message is AcOnMessage msg) {
        Console.WriteLine($"ACON: NN: {msg.NodeNumber}, EN: {msg.EventNumber}");
    }
};

messenger.MessageSent += (s, e) => {
    //Same as above, but is raised whenever a message is sent
    Console.WriteLine("-> " + e.Message);
};


// Send a message
await messenger.SendMessage(new AcOnMessage {
    NodeNumber = 2,
    EventNumber = 1
});
```

#### Conversations

The next level up provides a mechanism to send a message and expect a reply or replies:

```C#
var mm = new MessageManager(messenger);
// Wait for any number of replies to a message.  Returns after a timeout, default to 2 seconds - can be overridden.
// You can also pass the expected number of replies, and it will return immediately upon receiving that number
var qn = await mm.SendMessageWaitForReplies<QueryNodesResponseMessage>(new QueryAllNodesMessage());

foreach (var node in qn) {
    // Wait for a single reply to a message
    var nodeParamCount =
        await mm.SendMessageWaitForReply<ReadNodeParameterByIndexResponseMessage>(
            new ReadNodeParameterByIndexMessage(node.NodeNumber, 0));

    // Also wait for a single reply to a message, but note the use of a filter
    // to ensure we get a specific reply, and not just any ReadNodeParameterByIndexResponseMessage
    // This filter is available on all the SendMessageWaitFor... methods
    for (byte i = 1; i < nodeParamCount.ParameterValue; i++) {
        var param = await mm.SendMessageWaitForReply<ReadNodeParameterByIndexResponseMessage>(
            new ReadNodeParameterByIndexMessage(node.NodeNumber, i),
            m => m.ParameterIndex == i);
    }
}
```

#### Other

Optional constructor parameters are present on various classes to allow you to provide or inject [standard `ILogger<T>` loggers](https://docs.microsoft.com/en-us/dotnet/core/extensions/logging?tabs=command-line) if you wish to use the logging built in to the library.  A future version will allow for wiring up injection automatically via the standard `ServiceCollection` mechanism.