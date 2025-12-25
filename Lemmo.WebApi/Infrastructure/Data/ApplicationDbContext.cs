using Lemmo.WebApi.Entitys;
using Lemmo.WebApi.Entitys.Common;
using Lemmo.WebApi.Entitys.Students;
using Lemmo.WebApi.Entitys.TelegramSessions;
using Lemmo.WebApi.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Lemmo.WebApi.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentContact> StudentContacts { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<DefaultRoomSettings> DefaultRoomSettings { get; set; }
        public DbSet<DefaultUserLessonSettings> DefaultUserLessonSettings { get; set; }
        public DbSet<RoomScheduleTemplate> RoomScheduleTemplates { get; set; }
        public DbSet<ScheduleTemplateRule> ScheduleTemplateRules { get; set; }
        public DbSet<ScheduleException> ScheduleExceptions { get; set; }
        public DbSet<PersonalEvent> PersonalEvents { get; set; }
        public DbSet<PersonalEventTemplate> PersonalEventTemplates { get; set; }
        public DbSet<RecurrencePattern> RecurrencePatterns { get; set; }
        public DbSet<PersonalEventException> PersonalEventExceptions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<TelegramUserSession> TelegramUserSessions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            // Устанавливаем схему
            modelBuilder.HasDefaultSchema("lemmo");

            // Игнорируем базовый класс EntityBase
            modelBuilder.Ignore<EntityBase>();

            modelBuilder.Entity<TelegramUserSession>()
                .HasIndex(s => s.TelegramUserId)
                .IsUnique();

            // Конфигурации
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new StudentContactConfiguration());
            modelBuilder.ApplyConfiguration(new HomeworkConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new DefaultRoomSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new DefaultUserLessonSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new RoomScheduleTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleTemplateRuleConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleExceptionConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalEventConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalEventTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new RecurrencePatternConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalEventExceptionConfiguration());
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is EntityBase &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (EntityBase)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                }
                entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
    }
}
