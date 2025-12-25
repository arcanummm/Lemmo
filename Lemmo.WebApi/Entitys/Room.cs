using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.Students;

namespace Lemmo.WebApi.Entitys
{
    // Комната (Ученик)
    public class Room : EntityBase
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } = null!;

        public ICollection<Homework> Homeworks { get; set; } = [];
        public ICollection<Note> Notes { get; set; } = [];
        public ICollection<Lesson> Lessons { get; set; } = []; // Только фактические уроки
        public RoomScheduleTemplate? ScheduleTemplate { get; set; } // Шаблон повторяющихся уроков
        public DefaultRoomSettings DefaultSettings { get; set; } = null!; // Настройки по умолчанию
    }
}
