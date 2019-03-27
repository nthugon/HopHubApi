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
        Task<Review> GetByIdAsync(long id);
        Task<List<Review>> GetByBeerIdAsync(long id);
        Task DeleteAsync(Review review);
    }
}
