using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class PersonalEventExceptionConfiguration : IEntityTypeConfiguration<PersonalEventException>
    {
        public void Configure(EntityTypeBuilder<PersonalEventException> builder)
        {
            builder.ToTable("personal_event_exceptions");

            builder.HasKey(pee => pee.Id);
            builder.Property(pee => pee.Id).ValueGeneratedOnAdd();

            builder.Property(pee => pee.ExceptionDate)
                .IsRequired();

            builder.Property(pee => pee.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(pee => pee.NewDateTime);

            builder.Property(pee => pee.NewDuration)
                .HasColumnType("interval");

            builder.Property(pee => pee.Reason)
                .IsRequired()
                .HasMaxLength(500);

            // Связи
            builder.HasOne(pee => pee.Template)
                .WithMany(pet => pet.Exceptions)
                .HasForeignKey(pee => pee.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pee => pee.CreatedEvent)
                .WithMany()
                .HasForeignKey(pee => pee.CreatedEventId)
                .OnDelete(DeleteBehavior.SetNull);

            // Индексы
            builder.HasIndex(pee => pee.TemplateId);
            builder.HasIndex(pee => pee.ExceptionDate);
            builder.HasIndex(pee => pee.Type);
            builder.HasIndex(pee => new { pee.TemplateId, pee.ExceptionDate }).IsUnique();
        }
    }
}
