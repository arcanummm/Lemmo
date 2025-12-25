using Lemmo.WebApi.Entitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class DefaultRoomSettingsConfiguration : IEntityTypeConfiguration<DefaultRoomSettings>
    {
        public void Configure(EntityTypeBuilder<DefaultRoomSettings> builder)
        {
            builder.ToTable("default_room_settings");

            builder.HasKey(drs => drs.Id);
            builder.Property(drs => drs.Id).ValueGeneratedOnAdd();

            builder.Property(drs => drs.DefaultPricePerHour)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(drs => drs.DefaultDuration)
                .IsRequired()
                .HasColumnType("interval");

            // Связи
            builder.HasOne(drs => drs.Room)
                .WithOne(r => r.DefaultSettings)
                .HasForeignKey<DefaultRoomSettings>(drs => drs.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(drs => drs.RoomId).IsUnique();
        }
    }
}
