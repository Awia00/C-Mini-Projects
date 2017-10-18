using Microsoft.EntityFrameworkCore;

namespace LinkTo.Data
{
    public class LinkToContext : DbContext
    {
        public LinkToContext (DbContextOptions<LinkToContext> options)
            : base(options)
        {
        }

        public DbSet<LinkTo.Models.Link> Link { get; set; }
    }
}
