using Lemmo.WebApi.Persistance.Auth.Interfaces;
using Lemmo.WebApi.Persistance.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Lemmo.WebApi.Persistance.TelegramBot.Services
{
    public class TelegramMessageHandler(IServiceProvider serviceProvider) : ITelegramMessageHandler
    {
        //++
        public async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            if (message.From == null) return;

            if (message.Text == "/start")
            {
                await HandleStartCommandAsync(botClient, message, cancellationToken);
            }
            else if (message.Text == "register_command")
            {
                await HandleRegisterCallbackAsync(botClient, message, cancellationToken);
            }
            else
            {
                using var scope = serviceProvider.CreateScope();
                var registrationService = scope.ServiceProvider.GetRequiredService<ITelegramRegistrationService>();
                await registrationService.ProcessMessageAsync(botClient, message, cancellationToken);
            }
        }

        //++
        private async Task HandleStartCommandAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

            var telegramId = message.From!.Id;
            var isRegistered = await authManager.IsUserRegisteredByTelegramIdAsync(telegramId, cancellationToken);

            string response;
            InlineKeyboardMarkup inlineKeyboard;

            if (isRegistered)
            {
                response = $"👋 С возвращением, {message.From.FirstName}!\n" +
                          "Вы уже зарегистрированы в системе.";
                var loginLink = authManager.GetLoginLinkAsync();

                inlineKeyboard = new InlineKeyboardMarkup(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("🌐 Перейти на сайт", loginLink)
                    ],
                    [
                        InlineKeyboardButton.WithCallbackData("🔏 Забыли пароль?", loginLink)
                    ],
                    [
                        InlineKeyboardButton.WithCallbackData("‼️ Выход на всех устройствах", loginLink)
                    ],
                ]);
            }
            else
            {
                response = $"👋 Привет, {message.From.FirstName}!\n\n" +
                          "Я бот для регистрации в Lemmo.\n\n" +
                          "Нажмите кнопку ниже, чтобы начать регистрацию.";

                inlineKeyboard = new InlineKeyboardMarkup(
                [
                    [
                        InlineKeyboardButton.WithCallbackData("📝 Зарегистрироваться", "register_command")
                    ]
                ]);
            }

            await botClient.SendMessage(
                chatId: message.Chat.Id,
                text: response,
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken
            );
        }

        //++
        private async Task HandleRegisterCallbackAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var registrationService = scope.ServiceProvider.GetRequiredService<ITelegramRegistrationService>();
            await registrationService.StartRegistrationAsync(botClient, message, cancellationToken);
        }
    }
}
