using Lemmo.WebApi.Entitys.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lemmo.WebApi.Infrastructure.Data.Configurations
{
    public class StudentContactConfiguration : IEntityTypeConfiguration<StudentContact>
    {
        public void Configure(EntityTypeBuilder<StudentContact> builder)
        {
            builder.ToTable("student_contacts");

            builder.HasKey(sc => sc.Id);
            builder.Property(sc => sc.Id).ValueGeneratedOnAdd();

            builder.Property(sc => sc.Value)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(sc => sc.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();

            // Связи
            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.Contacts)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Индексы
            builder.HasIndex(sc => sc.StudentId);
            builder.HasIndex(sc => sc.Type);
        }
    }
}
