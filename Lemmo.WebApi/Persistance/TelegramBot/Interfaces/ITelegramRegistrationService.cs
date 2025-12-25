using Telegram.Bot;
using Telegram.Bot.Types;

namespace Lemmo.WebApi.Persistance.TelegramBot.Interfaces
{
    public interface ITelegramRegistrationService
    {
        Task StartRegistrationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
        Task ProcessMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
        Task CompleteRegistrationAsync(ITelegramBotClient botClient, long telegramId, string phoneNumber, string password, long chatId, CancellationToken cancellationToken);
    }
}
