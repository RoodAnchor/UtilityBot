using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Telegram.Bot;
using UtilityBot.Configuration;
using UtilityBot.Services;
using UtilityBot.Controllers;

namespace UtilityBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices(
                    (hostContext, services) => 
                        ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            Console.WriteLine("Сервис запущен");
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            AppConfig appConfig = BuildConfig();
            services.AddSingleton<AppConfig>(appConfig);

            services.AddSingleton<IStorage, SessionStorage>();
            services.AddSingleton<IService, MessageLengthCalculator>();
            services.AddSingleton<IService, SumCalculator>();

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => 
                new TelegramBotClient(appConfig.BotApiToken));

            services.AddHostedService<Bot>();
        }

        private static AppConfig BuildConfig()
        {
            return new AppConfig()
            {
                BotApiToken = "6744644124:AAG3Z0BlxXFhYJtASeXZnQH_tH9W8SaPizY"
            };
        }
    }
}