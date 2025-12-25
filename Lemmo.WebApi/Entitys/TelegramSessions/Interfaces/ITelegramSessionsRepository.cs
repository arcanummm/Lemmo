namespace Lemmo.WebApi.Entitys.TelegramSessions.Interfaces
{
    public interface ITelegramSessionsRepository
    {
        Task<TelegramUserSession?> GetByTelegramIdAsync(long telegramId, CancellationToken cancellationToken = default);
        Task DeleteByTelegramIdAsync(long telegramId, CancellationToken cancellationToken = default);
    }
}
