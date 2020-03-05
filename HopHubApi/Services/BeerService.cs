using HopHubApi.Models;
using HopHubApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace HopHubApi.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly ILogger _logger;

        public BeerService(IBeerRepository beerRepository, ILogger logger)
        {
            _beerRepository = beerRepository;
            _logger = logger;
        }
        public async Task<List<Beer>> GetAllAsync()
        {
            _logger.Information("Processing request to get all beers.");
            return await _beerRepository.GetAllAsync();    
        }

        public async Task<List<Beer>> GetAllWithReviewsAsync()
        {
            return await _beerRepository.GetAllWithReviewsAsync();
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            var beer = await _beerRepository.GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            return beer;
        }

        public async Task<Beer> CreateAsync(Beer beer)
        {
            return await _beerRepository.CreateAsync(beer);
        }

        public async Task UpdateAsync(int id, BeerRequest beerUpdate)
        {
            var beer = await _beerRepository.GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            beer.Name = string.IsNullOrEmpty(beerUpdate.Name) ? beer.Name : beerUpdate.Name;
            beer.Style = string.IsNullOrEmpty(beerUpdate.Style) ? beer.Style : beerUpdate.Style;
            beer.Brewery = string.IsNullOrEmpty(beerUpdate.Brewery) ? beer.Brewery : beerUpdate.Brewery;
            beer.Abv = (beerUpdate.Abv > 0) ? beerUpdate.Abv : beer.Abv;

            await _beerRepository.UpdateAsync(beer);
        }

        public async Task DeleteAsync(int id)
        {
            var beer = await _beerRepository.GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            await _beerRepository.DeleteAsync(beer);
        }        
    }
}