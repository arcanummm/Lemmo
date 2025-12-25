using Lemmo.WebApi.Entitys;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Comparers;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class RoomScheduleTemplateConfiguration : IEntityTypeConfiguration<RoomScheduleTemplate>
    {
        public void Configure(EntityTypeBuilder<RoomScheduleTemplate> builder)
        {
            builder.ToTable("room_schedule_templates");

            builder.HasKey(rst => rst.Id);
            builder.Property(rst => rst.Id).ValueGeneratedOnAdd();

            builder.Property(rst => rst.Name)
                .HasMaxLength(200);

            builder.Property(rst => rst.ValidFrom)
                .IsRequired()
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(rst => rst.ValidUntil)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            // Связи
            builder.HasOne(rst => rst.Room)
                .WithOne(r => r.ScheduleTemplate)
                .HasForeignKey<RoomScheduleTemplate>(rst => rst.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rst => rst.Rules)
                .WithOne(str => str.Template)
                .HasForeignKey(str => str.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(rst => rst.Exceptions)
                .WithOne(se => se.Template)
                .HasForeignKey(se => se.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(rst => rst.RoomId).IsUnique();
            builder.HasIndex(rst => rst.ValidFrom);
            builder.HasIndex(rst => rst.ValidUntil);
        }
    }
}
