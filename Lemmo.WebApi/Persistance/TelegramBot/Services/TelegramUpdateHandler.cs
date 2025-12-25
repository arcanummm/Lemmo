using Lemmo.WebApi.Persistance.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Lemmo.WebApi.Persistance.TelegramBot.Services
{
    public class TelegramUpdateHandler(IServiceScopeFactory scopeFactory, ILogger<TelegramUpdateHandler> logger) : ITelegramUpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        await HandleMessageAsync(botClient, update.Message!, cancellationToken);
                        break;
                    case UpdateType.CallbackQuery:
                        await HandleCallbackQueryAsync(botClient, update.CallbackQuery!, cancellationToken);
                        break;
                    default:
                        logger.LogDebug("Необработанный тип обновления: {UpdateType}", update.Type);
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка обработки обновления типа {UpdateType}", update.Type);
            }
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Ошибка при опросе Telegram");
            return Task.CompletedTask;
        }

        private async Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            using var scope = scopeFactory.CreateScope();
            var messageHandler = scope.ServiceProvider.GetRequiredService<ITelegramMessageHandler>();
            await messageHandler.HandleMessageAsync(botClient, message, cancellationToken);
        }

        private async Task HandleCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            logger.LogInformation("Получен CallbackQuery от пользователя {UserId}: {Data}",
                callbackQuery.From.Id, callbackQuery.Data);

            try
            {
                if (callbackQuery.Message != null)
                {
                    await botClient.EditMessageReplyMarkup(
                        chatId: callbackQuery.Message.Chat.Id,
                        messageId: callbackQuery.Message.MessageId,
                        replyMarkup: null,
                        cancellationToken: cancellationToken
                    );
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Не удалось обновить reply markup");
            }

            using var scope = scopeFactory.CreateScope();
            var messageHandler = scope.ServiceProvider.GetRequiredService<ITelegramMessageHandler>();

            var message = new Message
            {
                From = callbackQuery.From,
                Chat = callbackQuery.Message?.Chat ?? new Chat { Id = callbackQuery.From.Id },
                Text = callbackQuery.Data
            };

            await messageHandler.HandleMessageAsync(botClient, message, cancellationToken);

            await botClient.AnswerCallbackQuery(
                callbackQueryId: callbackQuery.Id,
                cancellationToken: cancellationToken
            );
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken) 
            => throw new NotImplementedException();
    }
}
