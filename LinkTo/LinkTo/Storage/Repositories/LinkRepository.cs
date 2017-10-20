using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using Storage.Models;

namespace Storage.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly LinkToContext _context;
        public LinkRepository(LinkToContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Link>> GetLink()
        {
            return await _context.Link.ToListAsync();
        }

        public async Task<Link> GetLink(int id)
        {
            var link = await _context.Link.SingleOrDefaultAsync(m => m.Id == id);

            return link;
        }

        public async Task<Link> PutLink(int id, Link link)
        {
            if (id != link.Id)
            {
                return null;
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
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return link;
        }

        public async Task<Link> PostLink(Link link)
        {
            // generate localUri
            link.LocalUri = "https://www.link-to.anderswind.dk/links/redirected/" + link.Name;

            _context.Link.Add(link);
            await _context.SaveChangesAsync();

            return link;
        }

        public async Task<Link> DeleteLink(int id)
        {
            var link = await _context.Link.SingleOrDefaultAsync(m => m.Id == id);
            if (link != null)
            {
                _context.Link.Remove(link);
                await _context.SaveChangesAsync();
            }
            return link;
        }

        private bool LinkExists(int id)
        {
            return _context.Link.Any(e => e.Id == id);
        }
    }
}
