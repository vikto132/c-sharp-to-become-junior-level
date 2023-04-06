using Core.Data.Junior;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class JuniorDbContext : DbContext
    {
        public JuniorDbContext(DbContextOptions<JuniorDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}