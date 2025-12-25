using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys.Students
{
    public class StudentContact : EntityBase
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public string Value { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
