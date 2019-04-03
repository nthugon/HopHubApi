using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HopHubApi.Models;
using HopHubApi.Repositories;
using HopHubApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HopHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [ApiController] allows us to omit binding source attributes and model validation
    
    // disable warning for unused var
    #pragma warning disable CS0168
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetAllAsync()
        {
            return await _reviewService.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetReview")]
        public async Task<ActionResult<Review>> GetByIdAsync(int id)
        {
            try
            {
                return await _reviewService.GetByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("/beer/{id}")]
        public async Task<ActionResult<List<Review>>> GetByBeerIdAsync(int id)
        {
            try
            {
                return await _reviewService.GetByBeerIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateAsync(Review review)
        {
            try
            {
                await _reviewService.CreateAsync(review);
                return CreatedAtRoute("GetReview", new { id = review.ReviewId }, review);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }


        }
    }
}
