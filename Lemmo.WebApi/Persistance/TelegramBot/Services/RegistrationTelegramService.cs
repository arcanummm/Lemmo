using Lemmo.WebApi.Entitys.TelegramSessions;
using Lemmo.WebApi.Entitys.TelegramSessions.Enums;
using Lemmo.WebApi.Persistance.Auth.Interfaces;
using Lemmo.WebApi.Persistance.TelegramBot.Interfaces;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Lemmo.WebApi.Persistance.TelegramBot.Services
{
    public class RegistrationTelegramService(IServiceScopeFactory scopeFactory) : ITelegramRegistrationService
    {
        //++
        public async Task StartRegistrationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
        try
        {
                var telegramId = message.From!.Id;

                using var scope = scopeFactory.CreateScope();

                var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

                var isAlreadyRegistered = await authManager.IsUserRegisteredByTelegramIdAsync(telegramId, cancellationToken);

                if (isAlreadyRegistered)
                {
                    await botClient.SendMessage(
                        chatId: message.Chat.Id,
                        text: "Вы уже зарегистрированы!",
                        cancellationToken: cancellationToken
                    );
                    return;
                }

                await authManager.CreateOrUpdateTelegramSessionAsync(
                    telegramId: telegramId,
                    state: RegistrationState.WaitingForPhone,
                    cancellationToken: cancellationToken);

                var keyboard = new ReplyKeyboardMarkup(
                [
                    [KeyboardButton.WithRequestContact("📱 Поделиться номером телефона")]
                ])
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                };

                await botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "📋 Регистрация\n\nПожалуйста, поделитесь вашим номером телефона, нажав кнопку ниже.",
                    replyMarkup: keyboard,
                    cancellationToken: cancellationToken
                );
            }
           catch(Exception ex){
                Console.WriteLine(ex);
           }
        }

        public async Task ProcessMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await TryDeleteMessageAsync(botClient, message.Chat.Id, message.MessageId, cancellationToken);

            var telergramId = message.From!.Id;

            using var scope = scopeFactory.CreateScope();

            var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

            var session = await authManager.GetSessionAsync(telergramId, cancellationToken);

            if (session == null)
            {
                return;
            }

            if (session.State == RegistrationState.WaitingForPhone)
            {
                await ProcessPhoneInputAsync(botClient, message, session, cancellationToken);
            }
            else if (session.State == RegistrationState.WaitingForPassword)
            {
                await ProcessPasswordInputAsync(botClient, message, session, cancellationToken);
            }
        }

        private async Task ProcessPhoneInputAsync(ITelegramBotClient botClient, Message message, TelegramUserSession session, CancellationToken cancellationToken)
        {
            var phoneNumber = ExtractPhoneNumber(message);

            if (!IsValidPhoneNumber(phoneNumber))
            {
                await botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "❌ Неверный формат номера телефона.",
                    cancellationToken: cancellationToken
                );
                return;
            }

            using var scope = scopeFactory.CreateScope();
            var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

            var isPhoneTaken = await authManager.IsPhoneNumberRegisteredAsync(phoneNumber, cancellationToken);

            if (isPhoneTaken)
            {
                await botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "❌ Этот номер телефона уже зарегистрирован.",
                    cancellationToken: cancellationToken
                );

                await authManager.DeleteTelegramSessionAsync(session.TelegramUserId, cancellationToken);
                return;
            }

            await authManager.CreateOrUpdateTelegramSessionAsync(
                session.TelegramUserId,
                phoneNumber: phoneNumber,
                state: RegistrationState.WaitingForPassword,
                cancellationToken: cancellationToken);

            await botClient.SendMessage(
                chatId: message.Chat.Id,
                text: $"✅ Номер принят: {phoneNumber}\n\n" +
                      "Теперь придумайте и введите пароль (минимум 6 символов)",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken
            );
        }

        private async Task ProcessPasswordInputAsync(ITelegramBotClient botClient, Message message, TelegramUserSession session, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(message.Text))
            {
                await botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "❌ Пожалуйста, введите пароль.",
                    cancellationToken: cancellationToken
                );
                return;
            }

            var password = message.Text.Trim();

            if (password.Length < 6)
            {
                await botClient.SendMessage(
                    chatId: message.Chat.Id,
                    text: "❌ Пароль должен содержать минимум 6 символов.",
                    cancellationToken: cancellationToken
                );
                return;
            }

            await CompleteRegistrationAsync(botClient, session.TelegramUserId, session.PhoneNumber!, password, message.Chat.Id, cancellationToken);
        }

        public async Task CompleteRegistrationAsync(ITelegramBotClient botClient, long telegramId, string phoneNumber, string password, long chatId, CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateScope();
            var authManager = scope.ServiceProvider.GetRequiredService<IAuthManager>();

            await authManager.CreateAccount(phoneNumber, password, telegramId, cancellationToken);

            var authLink = authManager.GetLoginLinkAsync();

            // Отправляем сообщение с кнопкой
            var inlineKeyboard = new InlineKeyboardMarkup(
            [
                [
                    InlineKeyboardButton.WithUrl("🌐 Перейти на сайт", authLink)
                ]
            ]);

            await botClient.SendMessage(
                chatId: chatId,
                text: $"🎉 Регистрация успешно завершена!\n\n" +
                      $"Нажмите кнопку ниже для перехода на сайт.",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken
            );
        }

        private string? ExtractPhoneNumber(Message message) => message.Contact?.PhoneNumber ?? message.Text?.Trim();

        private bool IsValidPhoneNumber(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            var cleanPhone = Regex.Replace(phone, @"[^\d+]", "");

            if (cleanPhone.StartsWith("+380") && cleanPhone.Length == 13) return true;
            if (cleanPhone.StartsWith("380") && cleanPhone.Length == 12) return true;
            if (cleanPhone.StartsWith("0") && cleanPhone.Length == 10) return true;
            if (cleanPhone.StartsWith("80") && cleanPhone.Length == 11) return true;

            return false;
        }

        private async Task<bool> TryDeleteMessageAsync(ITelegramBotClient botClient, long chatId, int messageId, CancellationToken cancellationToken)
        {
            try
            {
                await botClient.DeleteMessage(
                    chatId: chatId,
                    messageId: messageId,
                    cancellationToken: cancellationToken
                );
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
