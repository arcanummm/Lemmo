using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.TelegramId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            // Связи
            builder.HasOne(u => u.DefaultLessonSettings)
                .WithOne(us => us.User)
                .HasForeignKey<DefaultUserLessonSettings>(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Rooms)
                .WithOne(r => r.Owner)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.PersonalEventTemplates)
                .WithOne(pet => pet.User)
                .HasForeignKey(pet => pet.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.HasIndex(u => u.TelegramId).IsUnique();
            builder.HasIndex(u => u.CreatedAt);
        }
    }
}
