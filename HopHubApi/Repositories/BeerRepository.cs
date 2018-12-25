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

        public async Task<Beer> GetByIdAsync(long id)
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