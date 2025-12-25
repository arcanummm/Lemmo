using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.TelegramSessions.Enums;

namespace Lemmo.WebApi.Entitys.TelegramSessions
{
    public class TelegramUserSession : EntityBase
    {
        public long TelegramUserId { get; set; }
        public string? PhoneNumber { get; set; }
        public RegistrationState State { get; set; } = RegistrationState.None;
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    }
}
