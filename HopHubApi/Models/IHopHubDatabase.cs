using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopHubApi.Models
{
    public interface IHopHubDatabase : IDisposable
    {
        DbSet<Beer> Beers { get; set; }
        DbSet<Review> Reviews { get; set; }

        Task<bool> CheckDatabaseConnectionAsync();
        void ExecuteDatabaseMigration();

        Task<List<Beer>> GetAllBeersAsync();
        Task<List<Beer>> GetAllBeersWithReviewsAsync();
        Task<Beer> GetBeerByIdAsync(int id);
        Task<Beer> CreateBeerAsync(Beer beer);
        Task UpdateBeerAsync(Beer beer);
        Task DeleteBeerAsync(Beer beer);

        Task<List<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<List<Review>> GetReviewsByBeerIdAsync(int id);
        Task<Review> CreateReviewAsync(Review review);
        Task UpdateReviewAsync(Review review);
        Task DeleteReviewAsync(Review review);
    }
}
