using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.Enums;

namespace Lemmo.WebApi.Entitys
{
    // Фактический проведенный урок
    public class Lesson : EntityBase
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public Guid? TemplateId { get; set; }
        public RoomScheduleTemplate? Template { get; set; }
        public Guid? TemplateRuleId { get; set; }
        public ScheduleTemplateRule? TemplateRule { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Price { get; set; }

        public LessonStatus Status { get; set; } = LessonStatus.Completed; // Все сохраненные уроки - завершенные
        public string? Notes { get; set; }

        // Если урок перенесен/отменен из шаблона
        public bool IsModified { get; set; } // Изменен ли относительно шаблона
        public DateTimeOffset? OriginalPlannedTime { get; set; } // Когда должен был быть по шаблону
        public string? ModificationReason { get; set; }
    }
}
