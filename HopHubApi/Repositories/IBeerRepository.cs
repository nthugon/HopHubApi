using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;

namespace HopHubApi.Repositories
{
    public interface IBeerRepository
    {
        Task<List<Beer>> GetAllAsync();
        Task<List<Beer>> GetAllWithReviewsAsync();
        Task<Beer> GetByIdAsync(int id);
        Task<Beer> CreateAsync(Beer beer);
        Task UpdateAsync(Beer beer);
        Task DeleteAsync(Beer beer);         
    }
}