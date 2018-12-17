using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HopHubApi.Services
{
    public class BeersService : IBeersService
    {
        private readonly ApiContext _context;

        public BeersService(ApiContext context)
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

        public async Task UpdateAsync(long id, Beer beerUpdate)
        {
            var beer = await GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            beer.Name = beerUpdate.Name;
            beer.Style = beerUpdate.Style;
            beer.Brewery = beerUpdate.Brewery;
            beer.Abv = beerUpdate.Abv;

            _context.Beers.Update(beer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var beer = await GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
        }
        
    }
}