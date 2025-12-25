using Lemmo.WebApi.Entitys.Common;

namespace Lemmo.WebApi.Entitys
{
    public class DefaultUserLessonSettings : EntityBase
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public double DefaultPricePerHour { get; set; } = 300;
        public TimeSpan DefaultDuration { get; set; } = TimeSpan.FromHours(1);
    }
}
