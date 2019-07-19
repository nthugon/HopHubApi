using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HopHubApi.Models
{
    public class ApiContext : DbContext, IHopHubDatabase
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
            // only use for docker
            //this.Database.EnsureCreated();
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public async Task<List<Beer>> GetAllBeersAsync()
        {
            return await Beers.ToListAsync();
        }

        public async Task<List<Beer>> GetAllBeersWithReviewsAsync()
        {
            return await Beers.Include(b => b.Reviews)
                .ToListAsync();
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await Beers.FindAsync(id);
        }

        public async Task<Beer> CreateBeerAsync(Beer beer)
        {
            Beers.Add(beer);
            await SaveChangesAsync();

            return await GetBeerByIdAsync(beer.BeerId);
        }

        public async Task UpdateBeerAsync(Beer beer)
        {
            Beers.Update(beer);
            await SaveChangesAsync();
        }

        public async Task DeleteBeerAsync(Beer beer)
        {
            Beers.Remove(beer);
            await SaveChangesAsync();
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await Reviews.FindAsync(id);
        }

        public async Task<List<Review>> GetReviewsByBeerIdAsync(int id)
        {
            return await Reviews.Where(x => x.BeerId == id).ToListAsync();
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            Reviews.Add(review);
            await SaveChangesAsync();

            return await GetReviewByIdAsync(review.ReviewId);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            Reviews.Update(review);
            await SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Review review)
        {
            Reviews.Remove(review);
            await SaveChangesAsync();
        }
    }
}
