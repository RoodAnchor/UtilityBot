using Telegram.Bot;
using Telegram.Bot.Types;

using UtilityBot.Services;
using UtilityBot.Configuration;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер обрабатывающий запросы тип 
    /// сообщений которых не является текстовым.
    /// </summary>
    public class DefaultMessageController : BaseController
    {
        public override string CallbackMessage { get; set; }
            = "Получено сообщение не поддерживаемого формата";

        public DefaultMessageController(
            ITelegramBotClient telegramBotClient,
            AppConfig appConfig,
            IStorage storage
            ) : base(telegramBotClient, appConfig, storage) { }

        public override async Task Handle(
            CallbackQuery? callbackQuery,
            CancellationToken cancellationToken)
        {
            await base.Handle(
                callbackQuery,
                cancellationToken);
        }
    }
}
