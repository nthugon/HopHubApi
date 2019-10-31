using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HopHubApi.Models
{
    /// <inheritdoc cref="IHopHubDatabase" />
    public class ApiContext : DbContext, IHopHubDatabase
    {
        /// <inheritdoc />
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        /// <inheritdoc />
        public DbSet<Beer> Beers { get; set; }

        /// <inheritdoc />
        public DbSet<Review> Reviews { get; set; }

        /// <inheritdoc />
        public async Task<bool> CheckDatabaseConnectionAsync()
        {
            try
            {
                await Beers.AnyAsync();
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public void ExecuteDatabaseMigration()
        {
            if (!Database.GetPendingMigrations().Any())
            {
                return;
            }

            Database.Migrate();
        }

        /// <inheritdoc />
        public async Task<List<Beer>> GetAllBeersAsync()
        {
            return await Beers.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<Beer>> GetAllBeersWithReviewsAsync()
        {
            return await Beers.Include(b => b.Reviews)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await Beers.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<Beer> CreateBeerAsync(Beer beer)
        {
            Beers.Add(beer);
            await SaveChangesAsync();

            return await GetBeerByIdAsync(beer.BeerId);
        }

        /// <inheritdoc />
        public async Task UpdateBeerAsync(Beer beer)
        {
            Beers.Update(beer);
            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteBeerAsync(Beer beer)
        {
            Beers.Remove(beer);
            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await Reviews.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await Reviews.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<List<Review>> GetReviewsByBeerIdAsync(int id)
        {
            return await Reviews.Where(x => x.BeerId == id).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Review> CreateReviewAsync(Review review)
        {
            Reviews.Add(review);
            await SaveChangesAsync();

            return await GetReviewByIdAsync(review.ReviewId);
        }

        /// <inheritdoc />
        public async Task UpdateReviewAsync(Review review)
        {
            Reviews.Update(review);
            await SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteReviewAsync(Review review)
        {
            Reviews.Remove(review);
            await SaveChangesAsync();
        }
    }
}
