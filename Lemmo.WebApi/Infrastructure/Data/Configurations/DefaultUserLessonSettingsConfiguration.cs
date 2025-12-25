using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class DefaultUserLessonSettingsConfiguration : IEntityTypeConfiguration<DefaultUserLessonSettings>
    {
        public void Configure(EntityTypeBuilder<DefaultUserLessonSettings> builder)
        {
            builder.ToTable("default_user_lesson_settings");

            builder.HasKey(dus => dus.Id);
            builder.Property(dus => dus.Id).ValueGeneratedOnAdd();

            builder.Property(dus => dus.DefaultPricePerHour)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(dus => dus.DefaultDuration)
                .IsRequired()
                .HasColumnType("interval");

            // Связи
            builder.HasOne(dus => dus.User)
                .WithOne(u => u.DefaultLessonSettings)
                .HasForeignKey<DefaultUserLessonSettings>(dus => dus.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(dus => dus.UserId).IsUnique();
        }
    }
}
