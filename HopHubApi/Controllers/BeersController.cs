using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HopHubApi.Models;

namespace HopHubApi.Controllers
{
    [Route("api/[controller]")]
    public class BeersController : Controller
    {
        private readonly ApiContext _context;

        public BeersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Beer>> GetAll()
        {
            return _context.Beers.ToList();
        }

        [HttpGet("{id}", Name = "GetBeer")]
        public ActionResult<Beer> GetById(long id)
        {
            var beer = _context.Beers.Find(id);

            return beer == null ? (ActionResult<Beer>)NotFound() : (ActionResult<Beer>)beer;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Beer beer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Beers.Add(beer);
            _context.SaveChanges();

            return CreatedAtRoute("GetBeer", new { id = beer.Id }, beer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]Beer beerUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var beer = _context.Beers.Find(id);

            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdate.Name;
            beer.Style = beerUpdate.Style;
            beer.Brewery = beerUpdate.Brewery;
            beer.Abv = beerUpdate.Abv;

            _context.Beers.Update(beer);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var beer = _context.Beers.Find(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}