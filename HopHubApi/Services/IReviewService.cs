using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopHubApi.Models;

namespace HopHubApi.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task<List<Review>> GetByBeerIdAsync(int id);
        Task CreateAsync(Review review);
        Task UpdateAsync(int id, Review reviewUpdate);
        Task DeleteAsync(int id);
    }
}
