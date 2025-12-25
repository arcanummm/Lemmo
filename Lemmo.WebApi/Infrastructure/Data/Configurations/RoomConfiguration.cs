using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.HasOne(r => r.Owner)
                .WithMany(u => u.Rooms)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.DefaultSettings)
                .WithOne(drs => drs.Room)
                .HasForeignKey<DefaultRoomSettings>(drs => drs.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.ScheduleTemplate)
                .WithOne(rst => rst.Room)
                .HasForeignKey<RoomScheduleTemplate>(rst => rst.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Lessons)
                .WithOne(l => l.Room)
                .HasForeignKey(l => l.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Homeworks)
                .WithOne(h => h.Room)
                .HasForeignKey(h => h.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Notes)
                .WithOne(n => n.Room)
                .HasForeignKey(n => n.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(r => r.StudentId);
            builder.HasIndex(r => r.OwnerId);
            builder.HasIndex(r => r.CreatedAt);
        }
    }
}
