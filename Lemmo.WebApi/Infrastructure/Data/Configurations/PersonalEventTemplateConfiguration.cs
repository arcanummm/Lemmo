using Lemmo.WebApi.Entitys;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Comparers;
using Lemmo.WebApi.Infrastructure.Data.Configurations.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class PersonalEventTemplateConfiguration : IEntityTypeConfiguration<PersonalEventTemplate>
    {
        public void Configure(EntityTypeBuilder<PersonalEventTemplate> builder)
        {
            builder.ToTable("personal_event_templates");

            builder.HasKey(pet => pet.Id);
            builder.Property(pet => pet.Id).ValueGeneratedOnAdd();

            builder.Property(pet => pet.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(pet => pet.Description)
                .HasMaxLength(2000);

            builder.Property(pet => pet.Location)
                .HasMaxLength(500);

            builder.Property(pet => pet.Duration)
                .IsRequired()
                .HasColumnType("interval");

            builder.Property(pet => pet.ValidFrom)
                .IsRequired()
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(pet => pet.ValidUntil)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            // Связи
            builder.HasOne(pet => pet.User)
                .WithMany(u => u.PersonalEventTemplates)
                .HasForeignKey(pet => pet.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pet => pet.RecurrencePattern)
                .WithOne(rp => rp.Template)
                .HasForeignKey<RecurrencePattern>(rp => rp.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pet => pet.Exceptions)
                .WithOne(pee => pee.Template)
                .HasForeignKey(pee => pee.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(pet => pet.UserId);
            builder.HasIndex(pet => pet.ValidFrom);
            builder.HasIndex(pet => pet.ValidUntil);
        }
    }
}
