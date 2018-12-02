using Microsoft.EntityFrameworkCore;

namespace HopHubApi.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Beer> Beers { get; set; }
    }
}
