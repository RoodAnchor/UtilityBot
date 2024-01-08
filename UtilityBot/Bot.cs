using Microsoft.Extensions.Hosting;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using System.Transactions;
using UtilityBot.Configuration;
using UtilityBot.Controllers;

namespace UtilityBot
{
    public class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramClient;

        private InlineKeyboardController _inlineKeyboardController;
        private TextMessageController _textMessageController;
        private DefaultMessageController _defaultMessageController;

        public Bot(
            ITelegramBotClient telegramBotClient,
            InlineKeyboardController inlineKeyboardController,
            TextMessageController textMessageController,
            DefaultMessageController defaultMessageController)
        {
            _telegramClient = telegramBotClient;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessageController = textMessageController;
            _defaultMessageController = defaultMessageController;
        }

        protected override async Task ExecuteAsync(
            CancellationToken cancellationToken)
        {
            _telegramClient.StartReceiving(
                UpdateAsyncHandler,
                ErrorAsyncHandler,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: cancellationToken);

            Console.WriteLine("Бот запущен");
        }

        public async Task UpdateAsyncHandler(
            ITelegramBotClient telegramBotClient,
            Update update,
            CancellationToken cancellationToken)
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    await _inlineKeyboardController.Handle(
                        update.CallbackQuery, cancellationToken);

                    return;

                case UpdateType.Message:
                    switch (update.Message!.Type)
                    {
                        case MessageType.Text:
                            await _textMessageController.Handle(
                                update.Message, cancellationToken);
                            return;

                        default:
                            await _defaultMessageController.Handle(
                                update.Message, cancellationToken);
                            return;
                    }
            }
        }

        public Task ErrorAsyncHandler(
            ITelegramBotClient telegramBotClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException =>
                    $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);

            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
