using Lemmo.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id).HasName("id");

            builder.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(256);

            builder.Property(u => u.Role).HasColumnName("role").IsRequired();

            builder.Property(u => u.FirstName).HasColumnName("first_name").HasMaxLength(50);

            builder.Property(u => u.LastName).HasColumnName("last_name").HasMaxLength(50);

            builder.OwnsOne(u => u.Phone, pn =>
            {
                pn.Property(p => p.Value).HasColumnName("phone").IsRequired().HasMaxLength(15);

                pn.HasIndex(p => p.Value).IsUnique(); 
            });

            builder.OwnsOne(u => u.TelegramId, t =>
            {
                t.Property(p => p.Value).HasColumnName("telegram_id").IsRequired();

                t.HasIndex(p => p.Value).IsUnique(); 
            });

            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();

            builder.Property(u => u.UpdatedAt).HasColumnName("updated_at").IsRequired();

            builder.Property(u => u.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
        }
    }
}
