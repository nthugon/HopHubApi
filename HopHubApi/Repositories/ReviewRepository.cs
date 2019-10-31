using HopHubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopHubApi.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IHopHubDatabase _context;

        public ReviewRepository(IHopHubDatabase context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.GetAllReviewsAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.GetReviewByIdAsync(id);
        }

        public async Task<List<Review>> GetByBeerIdAsync(int id)
        {
            return await _context.GetReviewsByBeerIdAsync(id);
        }

        public async Task CreateAsync(Review review)
        {
            await _context.CreateReviewAsync(review);
        }

        public async Task UpdateAsync(Review review)
        {
            await _context.UpdateReviewAsync(review);
        }

        public async Task DeleteAsync(Review review)
        {
            await _context.DeleteReviewAsync(review);
        }
    }
}
