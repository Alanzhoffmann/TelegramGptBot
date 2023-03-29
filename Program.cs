using Microsoft.Extensions.Configuration;
using TelegramGptBot;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

using CancellationTokenSource cancellationTokenSource = new();

var client = new BotClient(config);

await client.StartAsync();

Console.ReadLine();

cancellationTokenSource.Cancel();