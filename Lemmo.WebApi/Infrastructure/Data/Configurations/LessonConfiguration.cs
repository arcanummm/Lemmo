using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("lessons");

            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.StartTime)
                .IsRequired();

            builder.Property(l => l.Duration)
                .IsRequired()
                .HasColumnType("interval");

            builder.Property(l => l.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(l => l.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(l => l.Notes)
                .HasMaxLength(2000);

            builder.Property(l => l.ModificationReason)
                .HasMaxLength(500);

            // Связи
            builder.HasOne(l => l.Room)
                .WithMany(r => r.Lessons)
                .HasForeignKey(l => l.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Template)
                .WithMany()
                .HasForeignKey(l => l.TemplateId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(l => l.TemplateRule)
                .WithMany()
                .HasForeignKey(l => l.TemplateRuleId)
                .OnDelete(DeleteBehavior.SetNull);

            // Индексы
            builder.HasIndex(l => l.RoomId);
            builder.HasIndex(l => l.StartTime);
            builder.HasIndex(l => l.Status);
            builder.HasIndex(l => new { l.RoomId, l.StartTime });
            builder.HasIndex(l => l.TemplateId);
            builder.HasIndex(l => l.OriginalPlannedTime);
        }
    }
}
