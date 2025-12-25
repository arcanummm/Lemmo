using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    // ШАБЛОН ПОВТОРЯЮЩИХСЯ ЛИЧНЫХ СОБЫТИЙ
    public class PersonalEventTemplate : EntityBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Location { get; set; }

        public TimeSpan Duration { get; set; }

        // Правила повторения
        public RecurrencePattern RecurrencePattern { get; set; } = null!;
        public DateOnly ValidFrom { get; set; }
        public DateOnly? ValidUntil { get; set; }

        public ICollection<PersonalEventException> Exceptions { get; set; } = new List<PersonalEventException>();
    }
}
