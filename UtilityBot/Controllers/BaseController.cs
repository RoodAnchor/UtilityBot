using Telegram.Bot.Types;
using Telegram.Bot;

using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using UtilityBot.Configuration;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Абстрактный базовый класс для контроллеров.
    /// Содержит основной функционал для обработки событий.
    /// </summary>
    public abstract class BaseController
    {
        #region Fields
        protected readonly IStorage storage;
        protected readonly AppConfig appConfig;
        private readonly ITelegramBotClient _telegramBotClient;
        #endregion Fields

        #region Properties
        public abstract String CallbackMessage { get; set; }
        #endregion Properties

        #region Delegates
        #endregion Delegates

        #region Events
        #endregion Events

        #region Constructors
        public BaseController(
            ITelegramBotClient telegramBotClient,
            AppConfig appConfig,
            IStorage storage)
        {
            _telegramBotClient = telegramBotClient;
            this.appConfig = appConfig;
            this.storage = storage;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Асинхронный метод обрабатывает событие 
        /// отправки клиенту обычного текстового сообщения
        /// </summary>
        /// <param name="message">Объект <seealso cref="Message"/> содержащий детали о сообщении.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><seealso cref="Task"/></returns>
        public virtual async Task Handle(
            Message message,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"{GetType().Name}: сообщение от клиента");

            await _telegramBotClient.SendTextMessageAsync(
                message.Chat.Id,
                CallbackMessage,
                cancellationToken: cancellationToken,
                parseMode: ParseMode.Html);
        }

        /// <summary>
        /// Асинхронный метод обрабатывает событие 
        /// отправки клиенту текстового сообщения с кнопками
        /// </summary>
        /// <param name="message">Объект <seealso cref="Message"/> содержащий детали о сообщении.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <param name="inlineKeyboardMarkup">Объект содержащий кнопки.</param>
        /// <returns><seealso cref="Task"/></returns>
        public virtual async Task Handle(
            Message message,
            CancellationToken cancellationToken,
            InlineKeyboardMarkup inlineKeyboardMarkup)
        {
            await _telegramBotClient.SendTextMessageAsync(
                message.Chat.Id,
                CallbackMessage,
                cancellationToken: cancellationToken,
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboardMarkup);
        }

        /// <summary>
        /// Асинхронный метод обрабатывает событие 
        /// нажатия кнопки на клиенте
        /// </summary>
        /// <param name="callbackQuery">Объект <seealso cref="CallbackQuery"/> 
        /// содержащий запроса от клиента.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns><seealso cref="Task"/></returns>
        public virtual async Task Handle(
            CallbackQuery? callbackQuery,
            CancellationToken cancellationToken)
        {
            if (callbackQuery == null) return;

            Console.WriteLine($"{GetType().Name}: нажатие кнопки {callbackQuery.Data}");

            await _telegramBotClient.SendTextMessageAsync(
                callbackQuery.From.Id,
                String.Format(CallbackMessage, callbackQuery.Data),
                cancellationToken: cancellationToken,
                parseMode: ParseMode.Html);
        }
        #endregion Methods
    }
}