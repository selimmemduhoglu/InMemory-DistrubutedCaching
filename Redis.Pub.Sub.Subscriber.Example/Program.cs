using StackExchange.Redis;

ConnectionMultiplexer _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = _connectionMultiplexer.GetSubscriber();

await subscriber.SubscribeAsync("mychannel", (channel, message) =>
{
    Console.WriteLine(message);
});

Console.Read();