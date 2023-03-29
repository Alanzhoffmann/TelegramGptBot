using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace TelegramGptBot
{
    internal class BotClient
    {
        public TelegramBotClient _client;

        public BotClient(IConfiguration configuration)
        {
            var keys = configuration?.GetSection("Keys").Get<Keys>()
                ?? throw new Exception("Missing or invalid config file");

            _client = new TelegramBotClient(keys.TelegramBot);
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            var me = await _client.GetMeAsync(cancellationToken: cancellationToken);
            Console.WriteLine(me.Username);

            _client.StartReceiving<UpdateHandler>(cancellationToken: cancellationToken);
        }
    }
}
