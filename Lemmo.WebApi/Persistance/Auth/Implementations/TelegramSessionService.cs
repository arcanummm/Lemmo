using Lemmo.WebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Lemmo.WebApi.Persistance.Auth.Implementations
{
    public class TelegramSessionService(ApplicationDbContext context, ILogger<TelegramSessionService> logger)
    {

        public async Task CleanupExpiredSessionsAsync(TimeSpan expirationTime)
        {
            var expirationDate = DateTime.UtcNow - expirationTime;

            var expiredSessions = await context.TelegramUserSessions
                .Where(s => s.LastActivity < expirationDate)
                .ToListAsync();

            if (expiredSessions.Count > 0)
            {
                context.TelegramUserSessions.RemoveRange(expiredSessions);
                await context.SaveChangesAsync();

                logger.LogInformation("Удалено {Count} просроченных сессий", expiredSessions.Count);
            }
        }
    }
}
