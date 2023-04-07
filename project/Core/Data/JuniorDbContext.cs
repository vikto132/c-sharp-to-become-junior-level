using Core.Data.Junior;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class JuniorDbContext : DbContext
    {
        public JuniorDbContext(DbContextOptions<JuniorDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany<Role>()
                .WithMany(x => x.Users);
        }
    }
}