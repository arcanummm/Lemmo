using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    // Настройки комнаты по умолчанию
    public class DefaultRoomSettings : EntityBase
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public double DefaultPricePerHour { get; set; }
        public TimeSpan DefaultDuration { get; set; }
    }
}
