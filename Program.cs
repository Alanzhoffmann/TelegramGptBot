using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
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

botClient.StartReceiving((client, update, token) =>
{
    if (update.Message?.Type == MessageType.Text)
    {
        var message = update.Message;

        var sentMessage = client.SendTextMessageAsync(message.Chat.Id, "Hello!", cancellationToken: token);
    }
},
(client, exception, token) =>
{

}, cancellationToken: cancellationTokenSource.Token);

Console.ReadLine();

cancellationTokenSource.Cancel();