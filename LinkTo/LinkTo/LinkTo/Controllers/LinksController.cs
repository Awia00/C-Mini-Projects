using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkTo.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkTo.Models;

namespace LinkTo.Controllers
{
    [Produces("application/json")]
    [Route("api/Links")]
    public class LinksController : Controller
    {
        private readonly LinkToContext _context;

        public LinksController(LinkToContext context)
        {
            _context = context;
        }

        // GET: api/Links
        [HttpGet]
        public IEnumerable<Link> GetLink()
        {
            return _context.Link;
        }
        
        // GET: api/Links/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLink([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var link = await _context.Link.SingleOrDefaultAsync(m => m.Id == id);

            if (link == null)
            {
                return NotFound();
            }

            return Ok(link);
        }

        // PUT: api/Links/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLink([FromRoute] int id, [FromBody] Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != link.Id)
            {
                return BadRequest();
            }

            _context.Entry(link).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Links
        [HttpPost]
        public async Task<IActionResult> PostLink([FromBody] Link link)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // generate localUri
            link.LocalUri = "https://www.link-to.anderswind.dk/links/redirected/" + link.Name;

            _context.Link.Add(link);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetLink", new { id = link.Id }, link);
        }

        // DELETE: api/Links/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var link = await _context.Link.SingleOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            _context.Link.Remove(link);
            await _context.SaveChangesAsync();

            return Ok(link);
        }

        private bool LinkExists(int id)
        {
            return _context.Link.Any(e => e.Id == id);
        }
    }
}