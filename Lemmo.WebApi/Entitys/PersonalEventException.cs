using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.Enums;

namespace Lemmo.WebApi.Entitys
{
    // ИСКЛЮЧЕНИЯ ДЛЯ ЛИЧНЫХ СОБЫТИЙ
    public class PersonalEventException : EntityBase
    {
        public Guid TemplateId { get; set; }
        public PersonalEventTemplate Template { get; set; } = null!;

        public DateTimeOffset ExceptionDate { get; set; }
        public PersonalEventExceptionType Type { get; set; }

        // Для переноса
        public DateTimeOffset? NewDateTime { get; set; }
        public TimeSpan? NewDuration { get; set; }

        public string Reason { get; set; } = string.Empty;

        // Ссылка на созданное событие
        public Guid? CreatedEventId { get; set; }
        public PersonalEvent? CreatedEvent { get; set; }
    }
}
