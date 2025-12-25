using Lemmo.WebApi.Entitys;
using Lemmo.WebApi.Entitys.TelegramSessions;
using Lemmo.WebApi.Entitys.TelegramSessions.Enums;
using Lemmo.WebApi.Infrastructure.Data;
using Lemmo.WebApi.Persistance.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lemmo.WebApi.Persistance.Auth.Implementations
{
    public class AuthManager(ApplicationDbContext dbContext, EfUnitOfWork efUnitOfWork, ITokenService tokenService, IHasher hasher, IConfiguration configuration) : IAuthManager
    {
        public async Task CreateAccount(string phoneNumber, string password, long telegramId, CancellationToken cancellationToken = default)
        {
            try
            {
                await efUnitOfWork.BeginAsync(cancellationToken);

                var user = new User
                {
                    TelegramId = telegramId,
                    PhoneNumber = phoneNumber,
                    PasswordHash = hasher.HashPassword(password),
                    CreatedAt = DateTime.UtcNow
                };

                dbContext.Users.Add(user);

                var session = await dbContext.TelegramUserSessions
                    .FirstOrDefaultAsync(s => s.TelegramUserId == telegramId, cancellationToken: cancellationToken)
                        ?? throw new InvalidOperationException("Telegram user session not found.");

                dbContext.TelegramUserSessions.Remove(session);

                await efUnitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await efUnitOfWork.RollbackAsync(cancellationToken);
            }
        }

        public async Task DeleteTelegramSessionAsync(long telegramId, CancellationToken cancellationToken = default)
        {
            var session = await dbContext.TelegramUserSessions
                    .FirstOrDefaultAsync(s => s.TelegramUserId == telegramId, cancellationToken: cancellationToken)
                        ?? throw new InvalidOperationException("Telegram user session not found.");
            dbContext.TelegramUserSessions.Remove(session);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateOrUpdateTelegramSessionAsync(long telegramId, string? phoneNumber = null, RegistrationState? state = null, CancellationToken cancellationToken = default)
        {
            var session = await dbContext.TelegramUserSessions
                    .FirstOrDefaultAsync(s => s.TelegramUserId == telegramId, cancellationToken: cancellationToken);

            if (session == null)
            {
                session = new TelegramUserSession
                {
                    TelegramUserId = telegramId,
                    PhoneNumber = phoneNumber,
                    State = state ?? RegistrationState.None
                };
                await dbContext.TelegramUserSessions.AddAsync(session, cancellationToken);
            }
            else
            {
                if (!string.IsNullOrEmpty(phoneNumber))
                    session.PhoneNumber = phoneNumber;

                if (state.HasValue)
                    session.State = state.Value;

                session.LastActivity = DateTime.UtcNow;
                dbContext.TelegramUserSessions.Update(session);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> IsPhoneNumberRegisteredAsync(string phoneNumber, CancellationToken cancellationToken = default)
            => await dbContext.Users.AsNoTracking().AnyAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);

        public async Task<TelegramUserSession?> GetSessionAsync(long telegramUserId, CancellationToken cancellationToken = default)
            => await dbContext.TelegramUserSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        public string GetLoginLinkAsync()
        {
            var baseUrl = configuration["Website:BaseUrl"] ?? throw new ArgumentNullException("");

            return $"{baseUrl}/auth";
        }

        public async Task<bool> IsUserRegisteredByTelegramIdAsync(long telegramId, CancellationToken cancellationToken = default)
            => await dbContext.Users.AsNoTracking().AnyAsync(u => u.TelegramId == telegramId, cancellationToken);

        public async Task<LoginResult> Login(string phoneNumber, string password, string device = "Unknow", CancellationToken cancellationToken = default)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber, cancellationToken);
            var loginResult = new LoginResult();

            if (user == null || !hasher.VerifyPassword(password, user.PasswordHash))
            {
                loginResult.IsFailure("Неверный логин или пароль.");
                return loginResult;
            }
            else
            {
                var accessToken = tokenService.GenerateAccessToken(user.Id.ToString(), user.PhoneNumber!);
                var (RefreshToken, ExpiresAt) = tokenService.GenerateRefreshToken();
                var refreshToken = new RefreshToken()
                {
                    TokenHash = hasher.HashRefreshToken(RefreshToken),
                    ExpiresAt = ExpiresAt,
                    Device = device,
                };

                user.RefreshTokens.Add(refreshToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                loginResult.IsSuccess(accessToken, RefreshToken);
                return loginResult;
            }
        }
    }
    public class LoginResult
    {
        public string? AccessToken { get; private set; } 

        public string? RefreshToken { get; private set; } 

        public bool IsSuccessful { get; private set; }

        public string? ErrorMessage { get; private set; }

        public void IsSuccess(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            IsSuccessful = true;
        }
        public void IsFailure(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccessful = false;
        }
    }
}
