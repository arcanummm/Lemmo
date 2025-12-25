using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class PersonalEventConfiguration : IEntityTypeConfiguration<PersonalEvent>
    {
        public void Configure(EntityTypeBuilder<PersonalEvent> builder)
        {
            builder.ToTable("personal_events");

            builder.HasKey(pe => pe.Id);
            builder.Property(pe => pe.Id).ValueGeneratedOnAdd();

            builder.Property(pe => pe.StartTime)
                .IsRequired();

            builder.Property(pe => pe.EndTime)
                .IsRequired();

            builder.Property(pe => pe.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pe => pe.Description)
                .HasMaxLength(2000);

            builder.Property(pe => pe.Location)
                .HasMaxLength(500);

            builder.Property(pe => pe.IsFromTemplate)
                .IsRequired()
                .HasDefaultValue(false);

            // Связи
            builder.HasOne(pe => pe.User)
                .WithMany()
                .HasForeignKey(pe => pe.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pe => pe.Template)
                .WithMany()
                .HasForeignKey(pe => pe.TemplateId)
                .OnDelete(DeleteBehavior.SetNull);

            // Индексы
            builder.HasIndex(pe => pe.UserId);
            builder.HasIndex(pe => pe.StartTime);
            builder.HasIndex(pe => pe.EndTime);
            builder.HasIndex(pe => new { pe.UserId, pe.StartTime });
        }
    }
}
