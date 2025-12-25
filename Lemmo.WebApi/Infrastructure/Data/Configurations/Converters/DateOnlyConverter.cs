using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations.Converters
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
        {
        }
    }
}
