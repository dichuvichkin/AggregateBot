using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AggregateBot.Services.TgUpdateService
{
    public interface ITgUpdateService
    {
        Task Update(Update update);
    }
}