
using StackExchange.Redis;

ConnectionMultiplexer _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber= _connectionMultiplexer.GetSubscriber();

while (true)
{
    Console.WriteLine("Mesaj : ");
    string _message = Console.ReadLine();
    await subscriber.PublishAsync("mychannel",_message);
}