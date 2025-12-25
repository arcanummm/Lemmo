using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class RecurrencePatternConfiguration : IEntityTypeConfiguration<RecurrencePattern>
    {
        public void Configure(EntityTypeBuilder<RecurrencePattern> builder)
        {
            builder.ToTable("recurrence_patterns");

            builder.HasKey(rp => rp.Id);
            builder.Property(rp => rp.Id).ValueGeneratedOnAdd();

            builder.Property(rp => rp.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(rp => rp.Interval)
                .IsRequired()
                .HasDefaultValue(1);

            // Хранение DaysOfWeek как JSON массива с ValueComparer
            builder.Property(rp => rp.DaysOfWeek)
                .HasConversion(
                    new ValueConverter<List<DayOfWeek>, string>(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                        v => JsonSerializer.Deserialize<List<DayOfWeek>>(v, (JsonSerializerOptions?)null) ?? new List<DayOfWeek>()
                    ),
                    new ValueComparer<List<DayOfWeek>>(
                        (c1, c2) => c1.SequenceEqual(c2), // Сравнение элементов
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Хеш-код
                        c => c.ToList() // Создание копии
                    )
                )
                .HasColumnType("jsonb");

            builder.Property(rp => rp.DayOfMonth);

            builder.Property(rp => rp.MonthlyType)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(rp => rp.EndDate);

            builder.Property(rp => rp.OccurrenceCount);

            // Связи
            builder.HasOne(rp => rp.Template)
                .WithOne(pet => pet.RecurrencePattern)
                .HasForeignKey<RecurrencePattern>(rp => rp.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(rp => rp.TemplateId).IsUnique();
            builder.HasIndex(rp => rp.Type);
        }
    }
}
