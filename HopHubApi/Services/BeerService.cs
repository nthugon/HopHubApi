using HopHubApi.Models;
using HopHubApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopHubApi.Services
{
    public class BeerService : IBeerService
    {
        private readonly IBeerRepository _beerRepository;

        public BeerService(IBeerRepository beerRepository)
        {
            _beerRepository = beerRepository;
        }
        public async Task<List<Beer>> GetAllAsync()
        {
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

        public async Task UpdateAsync(int id, Beer beerUpdate)
        {
            var beer = await _beerRepository.GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            beer.Name = beerUpdate.Name;
            beer.Style = beerUpdate.Style;
            beer.Brewery = beerUpdate.Brewery;
            beer.Abv = beerUpdate.Abv;

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