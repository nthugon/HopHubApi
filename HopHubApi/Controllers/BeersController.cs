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
    public class BeersController : Controller
    {
        private readonly ApiContext _context;
        private readonly IBeersService _beersService;

        public BeersController(ApiContext context, IBeersService beersService)
        {
            _context = context;
            _beersService = beersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Beer>>> GetAll()
        {
            return await _beersService.GetAllAsync();
        }

        [HttpGet("{id}", Name = "GetBeer")]
        public async Task<ActionResult<Beer>> GetById(long id)
        {
            var beer = await _beersService.GetByIdAsync(id);

            return beer == null ? (ActionResult<Beer>)NotFound() : (ActionResult<Beer>)beer;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beersService.CreateAsync(beer);
                return CreatedAtRoute("GetBeer", new { id = beer.Id }, beer);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]Beer beerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _beersService.UpdateAsync(id, beerUpdate);
                return NoContent();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException)
                {
                    return NotFound();
                }
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _beersService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                if (e is KeyNotFoundException)
                {
                    return NotFound();
                }
                return StatusCode(500);
            }
        }
    }
}