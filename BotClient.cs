using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace TelegramGptBot
{
    internal class BotClient
    {
        private readonly ILogger<BotClient> _logger;
        public TelegramBotClient _client;

        public BotClient(IConfiguration configuration, ILogger<BotClient> logger)
        {
            var keys = configuration?.GetSection("Keys").Get<Keys>()
                ?? throw new Exception("Missing or invalid config file");

            _client = new TelegramBotClient(keys.TelegramBot);
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            var me = await _client.GetMeAsync(cancellationToken: cancellationToken);
            _logger.LogInformation(me.Username);

            _client.StartReceiving<UpdateHandler>(cancellationToken: cancellationToken);
        }
    }
}
