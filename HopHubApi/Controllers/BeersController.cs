using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HopHubApi.Models;
using HopHubApi.Services;
using System.Threading.Tasks;
using System;
using Serilog;

namespace HopHubApi.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Handles CRUD requests related to Beers.
    /// </summary>
    [Route("api/[controller]")]
    // composed without [ApiController] attribute, so we must use binding source attributes and model validation   

    // disable warning for unused var
    #pragma warning disable CS0168 
    public class BeersController : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for the BeersController
        /// </summary>
        /// <param name="beerService">Business logic for Beer CRUD operations.</param>
        /// <param name="logger">Logging service.</param>
        public BeersController(IBeerService beerService, ILogger logger)
        {
            _beerService = beerService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all of the Beers.
        /// </summary>
        /// <returns>List of Beers.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult<List<Beer>>> GetAllAsync()
        {
            try
            {
                _logger.Information("Received request to get all beers.");
                return await _beerService.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Request to get all beers failed.");
                return StatusCode(500);
            }            
        }

        /// <summary>
        /// Gets all of the the Beers and their Reviews.
        /// </summary>
        /// <returns>List of Beers containing their reviews.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Gets requested Beer by Id.
        /// </summary>
        /// <param name="id">Unique identifier of the Beer.</param>
        /// <returns>The Beer requested.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        // endpoint is named to use CreatedAtRoute
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

        /// <summary>
        /// Creates a Beer from the information sent in the request.
        /// </summary>
        /// <param name="beer">Model representing the Beer's properties.</param>
        /// <returns>The Beer created.</returns>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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
                beer = await _beerService.CreateAsync(beer);
                _logger.Information($"Successfully created beer with name: {beer.Name}.");
                // uses return from db to send in the response; refers to named endpoint
                return CreatedAtRoute("GetBeer", new {id = beer.BeerId}, beer);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Request to create beer with name: {beer.Name} failed.");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates the properties of a Beer.
        /// </summary>
        /// <param name="id">Unique identifier of the Beer.</param>
        /// <param name="beerUpdate">Model representing the Beer's properties.</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Deletes a Beer.
        /// </summary>
        /// <param name="id">Uniuque indentifier of the Beer.</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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