using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys.Students
{
    // Ученик (без изменений)
    public class Student : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<StudentContact> Contacts { get; set; } = [];
    }
}
