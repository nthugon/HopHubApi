using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HopHubApi.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly ApiContext _context;
        public BeerRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<Beer>> GetAllAsync()
        {
            return await _context.Beers.ToListAsync();
        }

        public async Task<List<Beer>> GetAllWithReviewsAsync()
        {
            return await _context.Beers.Include(b => b.Reviews)
                            .ToListAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        public async Task CreateAsync(Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Beer beer)
        {
            _context.Beers.Update(beer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Beer beer)
        {
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        }        
    }
}