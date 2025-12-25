using Lemmo.WebApi.Entitys;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Comparers;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class ScheduleExceptionConfiguration : IEntityTypeConfiguration<ScheduleException>
    {
        public void Configure(EntityTypeBuilder<ScheduleException> builder)
        {
            builder.ToTable("schedule_exceptions");

            builder.HasKey(se => se.Id);
            builder.Property(se => se.Id).ValueGeneratedOnAdd();

            builder.Property(se => se.ExceptionDate)
                .IsRequired()
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(se => se.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(se => se.NewDateTime);

            builder.Property(se => se.NewDuration)
                .HasColumnType("interval");

            builder.Property(se => se.NewPrice)
                .HasColumnType("decimal(10,2)");

            builder.Property(se => se.Reason)
                .IsRequired()
                .HasMaxLength(500);

            // Связи
            builder.HasOne(se => se.Template)
                .WithMany(rst => rst.Exceptions)
                .HasForeignKey(se => se.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(se => se.CreatedLesson)
                .WithMany()
                .HasForeignKey(se => se.CreatedLessonId)
                .OnDelete(DeleteBehavior.SetNull);

            // Индексы
            builder.HasIndex(se => se.TemplateId);
            builder.HasIndex(se => se.ExceptionDate);
            builder.HasIndex(se => se.Type);
            builder.HasIndex(se => new { se.TemplateId, se.ExceptionDate }).IsUnique();
        }
    }
}
