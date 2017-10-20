using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Models;
using Storage.Repositories;

namespace LinkTo.Controllers
{
    [Produces("application/json")]
    [Route("api/Links")]
    public class LinksController : Controller
    {
        private readonly ILinkRepository _context;

        public LinksController(ILinkRepository context)
        {
            _context = context;
        }

        // GET: api/Links
        [HttpGet]
        public async Task<IEnumerable<Link>> GetLink()
        {
            return await _context.GetLink();
        }
        
        // GET: api/Links/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLink([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var link = await _context.GetLink(id);

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

            await _context.PutLink(id, link);

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

            link = await _context.PostLink(link);
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

            var link = await _context.DeleteLink(id);

            return Ok(link);
        }
    }
}