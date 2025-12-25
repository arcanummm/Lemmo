using Lemmo.WebApi.Entitys.TelegramSessions;
using Lemmo.WebApi.Entitys.TelegramSessions.Enums;
using Lemmo.WebApi.Persistance.Auth.Implementations;

namespace Lemmo.WebApi.Persistance.Auth.Interfaces
{
    public interface IAuthManager
    {
        Task CreateAccount(string phoneNumber, string password, long telegramId, CancellationToken cancellationToken = default);

        Task CreateOrUpdateTelegramSessionAsync(long telegramId, string? phoneNumber = null, RegistrationState? state = null, CancellationToken cancellationToken = default);

        Task<TelegramUserSession?> GetSessionAsync(long telegramUserId, CancellationToken cancellationToken = default);

        string GetLoginLinkAsync();

        Task<bool> IsUserRegisteredByTelegramIdAsync(long telegramId, CancellationToken cancellationToken = default);

        Task<bool> IsPhoneNumberRegisteredAsync(string phoneNumber, CancellationToken cancellationToken = default);

        Task DeleteTelegramSessionAsync(long telegramId, CancellationToken cancellationToken = default);

        Task<LoginResult> Login(string phoneNumber, string password, string device = "Unknow", CancellationToken cancellationToken = default);
    }
}
