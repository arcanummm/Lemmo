using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.Enums;

namespace Lemmo.WebApi.Entitys
{
    // ИСКЛЮЧЕНИЯ В ШАБЛОНЕ (отмена/перенос)
    public class ScheduleException : EntityBase
    {
        public Guid TemplateId { get; set; }
        public RoomScheduleTemplate Template { get; set; } = null!;

        public DateOnly ExceptionDate { get; set; }
        public ExceptionType Type { get; set; }

        // Для переноса
        public DateTimeOffset? NewDateTime { get; set; }
        public TimeSpan? NewDuration { get; set; }
        public double? NewPrice { get; set; }

        // Причина
        public string Reason { get; set; } = string.Empty;

        // Ссылка на созданный урок (если перенесен)
        public Guid? CreatedLessonId { get; set; }
        public Lesson? CreatedLesson { get; set; }
    }
}
