using System;
using System.Linq;
using System.Threading.Tasks;
using AggregateBot.Models.User;
using AggregateBot.Services.TgBotService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using User = AggregateBot.Models.User.User;

namespace AggregateBot.Services.TgUpdateService
{
    public class TgUpdateService : ITgUpdateService
    {
        private readonly ITgBotService _botService;
        private readonly UserContext _context;

        public TgUpdateService(ITgBotService botService,
            UserContext context)
        {
            _botService = botService;
            _context = context;
        }

        public async Task Update(Update update)
        {
            var client = _botService.Client;
            var message = update.Message.Text;
            var chatId = update.Message.Chat.Id;

            if (string.IsNullOrEmpty(message)) return;

            if (message.StartsWith("/start"))
            {
                var newUser = new User
                {
                    UserId = update.Message.Chat.Id,
                    PayExpireTime = DateTime.Now.AddDays(2),
                    Groups = Array.Empty<Group>()
                };

                await _context.Users.AddAsync(newUser);
                await client.SendTextMessageAsync(chatId, JsonConvert.SerializeObject(newUser));
            }
        }
    }
}