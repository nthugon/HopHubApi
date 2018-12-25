using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;
using HopHubApi.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Beer> GetByIdAsync(long id)
        {
            var beer = await _beerRepository.GetByIdAsync(id);

            if (beer == null)
            {
                throw new KeyNotFoundException();
            }

            return beer;
        }

        public async Task CreateAsync(Beer beer)
        {
            await _beerRepository.CreateAsync(beer);
        }

        public async Task UpdateAsync(long id, Beer beerUpdate)
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

        public async Task DeleteAsync(long id)
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