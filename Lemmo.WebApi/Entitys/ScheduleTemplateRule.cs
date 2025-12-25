using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    // Правило в шаблоне (день+время)
    public class ScheduleTemplateRule : EntityBase
    {
        public Guid TemplateId { get; set; }
        public RoomScheduleTemplate Template { get; set; } = null!;

        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public double Price { get; set; }

        // Можно отключить отдельное правило без удаления
        public bool IsActive { get; set; } = true;
    }
}
