using System.Threading.Tasks;
using AggregateBot.Services.TgUpdateService;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace AggregateBot.Controllers
{
    [Route("api/[controller]")]
    public class TelegramController : Controller
    {
        private readonly ITgUpdateService _updateService;

        public TelegramController(ITgUpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] Update update)
        {
            await _updateService.Update(update);
            return Ok();
        }
    }
}