using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopHubApi.Models;
using HopHubApi.Repositories;

namespace HopHubApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _reviewRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Review review)
        {
            await _reviewRepository.CreateAsync(review);
        }

        public async Task<List<Review>> GetByBeerIdAsync(int id)
        {
            return await _reviewRepository.GetByBeerIdAsync(id);
        }

        public async Task DeleteAsync(Review review)
        {
            await _reviewRepository.DeleteAsync(review);
        }
    }
}
