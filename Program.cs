using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TelegramGptBot;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var logger = loggerFactory.CreateLogger<BotClient>();
var client = new BotClient(config, logger);

using CancellationTokenSource cancellationTokenSource = new();

await client.StartAsync(cancellationTokenSource.Token);

Console.ReadLine();

cancellationTokenSource.Cancel();