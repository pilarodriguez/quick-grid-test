using Microsoft.EntityFrameworkCore;
using QuickGridTest.Data.Entities;

namespace QuickGridTest.Data.Contexts
{
    public class QuickGridDbContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }

        public QuickGridDbContext(DbContextOptions<QuickGridDbContext> options) : base(options) { }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
