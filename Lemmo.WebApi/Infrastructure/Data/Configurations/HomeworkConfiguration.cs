using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.ToTable("homeworks");

            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).ValueGeneratedOnAdd();

            builder.Property(h => h.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(h => h.Deadline)
                .IsRequired();

            // Связи
            builder.HasOne(h => h.Room)
                .WithMany(r => r.Homeworks)
                .HasForeignKey(h => h.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(h => h.RoomId);
            builder.HasIndex(h => h.Deadline);
        }
    }
}
