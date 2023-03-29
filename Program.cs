using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using TelegramGptBot;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var keys = config?.GetSection("Keys").Get<Keys>()
    ?? throw new Exception("Missing or invalid config file");

var botClient = new TelegramBotClient(keys.TelegramBot);
var me = await botClient.GetMeAsync();

Console.WriteLine(me.Username);

using CancellationTokenSource cancellationTokenSource = new();

botClient.StartReceiving<UpdateHandler>(cancellationToken: cancellationTokenSource.Token);

Console.ReadLine();

cancellationTokenSource.Cancel();