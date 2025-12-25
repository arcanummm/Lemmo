using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    public class Homework : EntityBase
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public string FileUrl { get; set; } = string.Empty;
        public DateTimeOffset Deadline { get; set; }
    }
}
