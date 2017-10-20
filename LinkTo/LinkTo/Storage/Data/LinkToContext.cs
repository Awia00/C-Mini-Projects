using Microsoft.EntityFrameworkCore;

namespace Storage.Data
{
    public class LinkToContext : DbContext
    {
        public LinkToContext (DbContextOptions<LinkToContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=LinkTo.db");
        }

        public DbSet<Models.Link> Link { get; set; }
    }
}
