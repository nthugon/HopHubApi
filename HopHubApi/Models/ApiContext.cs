using Microsoft.EntityFrameworkCore;

namespace HopHubApi.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            // only use for docker
            //this.Database.EnsureCreated();
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
