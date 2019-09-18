using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;
using Serilog;

namespace HopHubApi.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly IHopHubDatabase _context;
        private readonly ILogger _logger;
        public BeerRepository(IHopHubDatabase context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Beer>> GetAllAsync()
        {
            _logger.Information("Retrieving all beers.");
            return await _context.GetAllBeersAsync();
        }

        public async Task<List<Beer>> GetAllWithReviewsAsync()
        {
            return await _context.GetAllBeersWithReviewsAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            return await _context.GetBeerByIdAsync(id);
        }

        public async Task<Beer> CreateAsync(Beer beer)
        {
            return await _context.CreateBeerAsync(beer);
        }

        public async Task UpdateAsync(Beer beer)
        {
            await _context.UpdateBeerAsync(beer);
        }

        public async Task DeleteAsync(Beer beer)
        {
            await _context.DeleteBeerAsync(beer);
        }
    }
}