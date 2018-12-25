using System.Collections.Generic;
using System.Threading.Tasks;
using HopHubApi.Models;

namespace HopHubApi.Repositories
{
    public interface IBeerRepository
    {
        Task<List<Beer>> GetAllAsync();
        Task<Beer> GetByIdAsync(long id);
        Task CreateAsync(Beer beer);
        Task UpdateAsync(Beer beer);
        Task DeleteAsync(Beer beer);         
    }
}