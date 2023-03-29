using Microsoft.Extensions.Configuration;
using TelegramGptBot;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var keys = config?.GetSection("Keys").Get<Keys>()
    ?? throw new Exception("Missing or invalid config file");