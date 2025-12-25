using Lemmo.Domain.Users;
using Lemmo.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Lemmo.Persistence
{
    public class LemmoDbContext(DbContextOptions<LemmoDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
