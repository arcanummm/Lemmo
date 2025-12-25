using Lemmo.WebApi.Entitys;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Comparers;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class ScheduleTemplateRuleConfiguration : IEntityTypeConfiguration<ScheduleTemplateRule>
    {
        public void Configure(EntityTypeBuilder<ScheduleTemplateRule> builder)
        {
            builder.ToTable("schedule_template_rules");

            builder.HasKey(str => str.Id);
            builder.Property(str => str.Id).ValueGeneratedOnAdd();

            builder.Property(str => str.DayOfWeek)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(str => str.StartTime)
                .IsRequired()
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();

            builder.Property(str => str.Duration)
                .IsRequired()
                .HasColumnType("interval");

            builder.Property(str => str.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(str => str.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Связи
            builder.HasOne(str => str.Template)
                .WithMany(rst => rst.Rules)
                .HasForeignKey(str => str.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(str => str.TemplateId);
            builder.HasIndex(str => str.DayOfWeek);
            builder.HasIndex(str => str.IsActive);
            builder.HasIndex(str => new { str.TemplateId, str.DayOfWeek, str.StartTime }).IsUnique();
        }
    }
}
