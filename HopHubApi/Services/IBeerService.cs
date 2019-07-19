using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;

namespace HopHubApi.Services
{
    public interface IBeerService
    {
         Task<List<Beer>> GetAllAsync();
         Task<List<Beer>> GetAllWithReviewsAsync();
         Task<Beer> GetByIdAsync(int id);
         Task<Beer> CreateAsync(Beer beer);
         Task UpdateAsync(int id, Beer beerUpdate);
         Task DeleteAsync(int id);
    }
}