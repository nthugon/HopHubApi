using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HopHubApi.Models;
using HopHubApi.Services;
using System.Threading.Tasks;
using System;
using Serilog;

namespace HopHubApi.Controllers
{
    [Route("api/[controller]")]
    // composed without [ApiController] attribute, so we must use binding source attributes and model validation
    // using CreatedAtRoute for endpoint must be named

    // disable warning for unused var
    #pragma warning disable CS0168 
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly ILogger _logger;

        public BeersController(IBeerService beerService, ILogger logger)
        {
            _beerService = beerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Beer>>> GetAllAsync()
        {
            try
            {
                _logger.Information("Getting all beers.");
                return await _beerService.GetAllAsync();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Request to get all beers failed.");
                return StatusCode(500);
            }
            
        }

        [HttpGet("reviews")]
        public async Task<ActionResult<List<Beer>>> GetAllWithReviewsAsync()
        {
            try
            {
                _logger.Information("Getting all beers with reviews.");
                return await _beerService.GetAllWithReviewsAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Request to get all beers with reviews failed.");
                return StatusCode(500);
            }
        }

        [HttpGet("{id}", Name = "GetBeer")]
        public async Task<ActionResult<Beer>> GetByIdAsync([FromRoute]int id)
        {
            try
            {
                _logger.Information($"Getting beer with id: {id}.");
                return await _beerService.GetByIdAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.Warning(ex, $"Beer with id: {id} could not be found");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Request to get beer with id: {id} failed.");
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Beer>> CreateAsync([FromBody]Beer beer)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warning("Request to create beer has been denied. Beer model is invalid");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.Information($"Creating beer with name: {beer.Name}.");
                await _beerService.CreateAsync(beer);
                _logger.Information($"Successfully created beer with name: {beer.Name}.");
                return CreatedAtRoute("GetBeer", new { id = beer.BeerId }, beer);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Request to create beer with name: {beer.Name} failed.");
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody]Beer beerUpdate)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warning("Request to update beer has been denied. Beer model is invalid");
                return BadRequest(ModelState);
            }

            try
            {
                _logger.Information($"Updating beer with id: {id}.");
                await _beerService.UpdateAsync(id, beerUpdate);
                _logger.Information($"Successfully updated beer with id: {id}.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.Warning(ex, $"Beer with id: {id} could not be updated, as it was not found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Request to update beer with id: {id} failed.");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute]int id)
        {
            try
            {
                _logger.Information($"Deleting beer with id: {id}.");
                await _beerService.DeleteAsync(id);
                _logger.Information($"Successfully deleted beer with id: {id}.");
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.Warning(ex, $"Beer with id: {id} could not be deleted, as it was not found.");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Request to delete beer with id: {id} failed.");
                return StatusCode(500);
            }
        }
    }
}