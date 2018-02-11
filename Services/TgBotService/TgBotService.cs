using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace AggregateBot.Services.TgBotService
{
    public class TgBotService : ITgBotService
    {
        private readonly BotConfiguration _config;

        public TgBotService(
            IOptions<BotConfiguration> config)
        {
            _config = config.Value;
            Client = new TelegramBotClient(_config.BotToken);
        }

        public TelegramBotClient Client { get; }
    }
}