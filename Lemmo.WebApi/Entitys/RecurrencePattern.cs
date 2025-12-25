using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.Enums;

namespace Lemmo.WebApi.Entitys
{
    // ПРАВИЛО ПОВТОРЕНИЯ ДЛЯ ЛИЧНЫХ СОБЫТИЙ
    public class RecurrencePattern : EntityBase
    {
        public Guid TemplateId { get; set; }
        public PersonalEventTemplate Template { get; set; } = null!;

        public RecurrenceType Type { get; set; }
        public int Interval { get; set; } = 1;

        // Для Weekly
        public List<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();

        // Для Monthly
        public int? DayOfMonth { get; set; }
        public MonthlyRecurrenceType? MonthlyType { get; set; } // DayOfMonth или DayOfWeek

        // Ограничения
        public DateTimeOffset? EndDate { get; set; }
        public int? OccurrenceCount { get; set; }
    }
}
