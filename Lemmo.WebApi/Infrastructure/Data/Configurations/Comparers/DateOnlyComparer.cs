using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations.Comparers
{
    public class DateOnlyComparer : ValueComparer<DateOnly>
    {
        public DateOnlyComparer() : base(
            (d1, d2) => d1 == d2,
            d => d.GetHashCode())
        {
        }
    }
}
