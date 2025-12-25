using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    public class Note : EntityBase
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public string FileUrl { get; set; } = string.Empty;
    }
}
