using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("notes");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).ValueGeneratedOnAdd();

            builder.Property(n => n.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            // Связи
            builder.HasOne(n => n.Room)
                .WithMany(r => r.Notes)
                .HasForeignKey(n => n.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(n => n.RoomId);
        }
    }
}
