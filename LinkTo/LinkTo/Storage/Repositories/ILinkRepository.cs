using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Storage.Models;

namespace Storage.Repositories
{
    public interface ILinkRepository
    {
        Task<IEnumerable<Link>> GetLink();
        Task<Link> GetLink(int id);
        Task<Link> GetLink(string name);
        Task<Link> PutLink(int id, Link link);
        Task<Link> PostLink(Link link);
        Task<Link> DeleteLink(int id);
    }
}
