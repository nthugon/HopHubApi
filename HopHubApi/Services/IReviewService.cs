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
        Task CreateAsync(Review review);
        Task<Review> GetByIdAsync(int id);
        Task<List<Review>> GetByBeerIdAsync(int id);
        Task DeleteAsync(Review review);
    }
}
