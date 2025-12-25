using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations.Comparers
{
    public class TimeOnlyComparer : ValueComparer<TimeOnly>
    {
        public TimeOnlyComparer() : base(
            (t1, t2) => t1 == t2,
            t => t.GetHashCode())
        {
        }
    }
}
