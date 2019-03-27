using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HopHubApi.Models;
using HopHubApi.Services;
using System.Threading.Tasks;
using System;

namespace HopHubApi.Controllers
{
    [Route("api/[controller]")]
    //composed without [ApiController] attribute so must use binding source attributes and model validation 
    public class BeersController : Controller
    {
        private readonly IBeerService _beerService;

        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Beer>>> GetAllAsync()
        {
            return await _beerService.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetBeer")]
        public async Task<ActionResult<Beer>> GetByIdAsync([FromRoute]long id)
        {
            try
            {
                return await _beerService.GetByIdAsync(id);
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
        public async Task<ActionResult<Beer>> CreateAsync([FromBody]Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beerService.CreateAsync(beer);
                return CreatedAtRoute("GetBeer", new { id = beer.Id }, beer);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(long id, [FromBody]Beer beerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beerService.UpdateAsync(id, beerUpdate);
                return NoContent();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]long id)
        {
            try
            {
                await _beerService.DeleteAsync(id);
                return NoContent();
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
    }
}