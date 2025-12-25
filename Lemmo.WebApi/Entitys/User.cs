using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    // Пользователь (Учитель)
    public class User : EntityBase
    {
        public long TelegramId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Room> Rooms { get; set; } = [];

        public DefaultUserLessonSettings DefaultLessonSettings { get; set; } = new();

        public ICollection<PersonalEventTemplate> PersonalEventTemplates { get; set; } = [];

        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
