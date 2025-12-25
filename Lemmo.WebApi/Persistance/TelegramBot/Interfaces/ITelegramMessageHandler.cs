using Telegram.Bot;
using Telegram.Bot.Types;

namespace Lemmo.WebApi.Persistance.TelegramBot.Interfaces
{
    public interface ITelegramMessageHandler
    {
        Task HandleMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
    }
}
