using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopHubApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HopHubApi.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApiContext _context;

        public ReviewRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task CreateAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetByBeerIdAsync(int id)
        {
            return await _context.Reviews.Where(x => x.BeerId == id).ToListAsync();
        }

        public async Task DeleteAsync(Review review)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
