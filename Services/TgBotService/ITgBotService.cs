using Telegram.Bot;

namespace AggregateBot.Services.TgBotService
{
    public interface ITgBotService
    {
        TelegramBotClient Client { get; }
    }
}