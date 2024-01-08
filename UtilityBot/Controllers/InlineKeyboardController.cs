using Telegram.Bot.Types;
using Telegram.Bot;

using UtilityBot.Services;
using UtilityBot.Configuration;
using UtilityBot.Enums;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер обработки событий 
    /// нажатия кнопки на клиенте
    /// </summary>
    public class InlineKeyboardController : BaseController
    {
        public override string CallbackMessage { get; set; }
            = "Обнаружено нажатие на кнопку {0}";

        public InlineKeyboardController(
            ITelegramBotClient telegramBotClient,
            IStorage memoryStorage,
            AppConfig appConfig
            ) : base(telegramBotClient, appConfig, memoryStorage) { }

        public override async Task Handle(
            CallbackQuery? callbackQuery,
            CancellationToken cancellationToken)
        {
            if (callbackQuery == null) return;

            ButtonsEnum selection = Enum.Parse<ButtonsEnum>(callbackQuery.Data);

            storage.GetSession(callbackQuery.From.Id)
                .Service = selection;

            String seletedFunctionText = selection switch
            {
                ButtonsEnum.MessageLength => "Подсчёт количества символов",
                ButtonsEnum.SumCalculator => "Сумма чисел",
                _ => String.Empty
            };

            String hintMessage = selection switch
            {
                ButtonsEnum.MessageLength => "Теперь отправьте текстовое сообщение для подсчёта символов",
                ButtonsEnum.SumCalculator => "Теперь отправьте числа (через пробел) для подсчёта суммы. Пример: 2 6 23 51",
                _ => String.Empty
            };

            CallbackMessage = $"<b>Выбрана функция - {seletedFunctionText}.{Environment.NewLine}</b>Можно поменять в главном меню{Environment.NewLine}";
            CallbackMessage += $"{Environment.NewLine}{hintMessage}";

            await base.Handle(
                callbackQuery,
                cancellationToken);
        }
    }
}
