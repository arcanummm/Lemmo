using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    // ЛИЧНЫЕ СОБЫТИЯ УЧИТЕЛЯ
    public class PersonalEvent : EntityBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }

        // Для повторяющихся событий
        public bool IsFromTemplate { get; set; }
        public Guid? TemplateId { get; set; }
        public PersonalEventTemplate? Template { get; set; }
    }
}
