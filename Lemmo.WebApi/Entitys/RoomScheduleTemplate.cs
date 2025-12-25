using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    // ШАБЛОН ПОВТОРЯЮЩИХСЯ УРОКОВ ДЛЯ КОМНАТЫ
    public class RoomScheduleTemplate : EntityBase
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public string? Name { get; set; }
        public DateOnly ValidFrom { get; set; }
        public DateOnly? ValidUntil { get; set; }

        public ICollection<ScheduleTemplateRule> Rules { get; set; } = [];
        public ICollection<ScheduleException> Exceptions { get; set; } = [];
    }
}
