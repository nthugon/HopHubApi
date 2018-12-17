using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;

namespace HopHubApi.Services
{
    public interface IBeersService
    {
         Task<List<Beer>> GetAllAsync();
         Task<Beer> GetByIdAsync(long id);
         Task CreateAsync(Beer beer);
         Task UpdateAsync(long id, Beer beerUpdate);
         Task DeleteAsync(long id);
    }
}