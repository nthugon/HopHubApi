using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopHubApi.Models;

namespace HopHubApi.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(long id);
        Task CreateAsync(Review review);
        Task<List<Review>> GetByBeerIdAsync(long id);
        Task DeleteAsync(Review review);
    }
}
