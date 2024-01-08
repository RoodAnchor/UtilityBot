using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

using UtilityBot.Configuration;
using UtilityBot.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер обработки обычных текстовых сообщений
    /// </summary>
    public class TextMessageController : BaseController
    {
        private IService _messageLengthCalculator;
        private IService _sumCalculator;

        public override string CallbackMessage { get; set; }
            = "Получено текстовое сообщение";

        public TextMessageController(
            ITelegramBotClient telegramBotClient,
            AppConfig appConfig,
            IStorage storage,
            IService messageLengthCalculator,
            IService sumCalculator
            ) : base(telegramBotClient, appConfig, storage) 
        {
            _messageLengthCalculator = messageLengthCalculator;
            _sumCalculator = sumCalculator;
        }

        public override async Task Handle(Message message, CancellationToken cancellationToken)
        {
            switch (message.Text)
            {
                case "/start":
                    CallbackMessage = $"<b>Бот выполняет 2 функции</b> {Environment.NewLine}{Environment.NewLine}- Подсчёт символов в тексте{Environment.NewLine}- Вычисляет сумму чисел{Environment.NewLine}";

                    List<InlineKeyboardButton[]> buttons =
                        new List<InlineKeyboardButton[]>();

                    buttons.Add(new[] {
                        InlineKeyboardButton.WithCallbackData(
                            "Количество символов", ButtonsEnum.MessageLength.ToString()),
                        InlineKeyboardButton.WithCallbackData(
                            "Сумма чисел", ButtonsEnum.SumCalculator.ToString())
                    });

                    await base.Handle(
                        message,
                        cancellationToken,
                        new InlineKeyboardMarkup(buttons));

                    break;

                default:
                    ButtonsEnum selection = storage.GetSession(message.From.Id).Service;

                    switch (selection)
                    {
                        case ButtonsEnum.MessageLength:
                            CallbackMessage = _messageLengthCalculator.GetResult(message.Text);

                            break;

                        case ButtonsEnum.SumCalculator:
                            CallbackMessage = _sumCalculator.GetResult(message.Text);

                            break;
                    }

                    await base.Handle(message, cancellationToken);
                    break;
            }
        }
    }
}
