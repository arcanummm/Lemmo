using Lemmo.WebApi.Persistance.TelegramBot.Interfaces;
using Lemmo.WebApi.Persistance.TelegramBot.Services;
using Telegram.Bot;

namespace Lemmo.WebApi.Persistance.TelegramBot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            // Telegram Bot Client
            services.AddSingleton<ITelegramBotClient>(sp =>
            {
                var token = configuration["TelegramBot:Token"];
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("Telegram Bot Token не настроен");

                return new TelegramBotClient(token);
            });

            // Сервисы
            services.AddScoped<ITelegramMessageHandler, TelegramMessageHandler>();
            services.AddScoped<ITelegramRegistrationService, RegistrationTelegramService>();

            // Обработчики
            services.AddSingleton<ITelegramUpdateHandler, TelegramUpdateHandler>();

            // Hosted Service
            services.AddHostedService<TelegramBotHostedService>();

            return services;
        }
    }
}
