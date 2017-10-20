using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OutUri { get; set; }
        public string LocalUri { get; set; }
    }
}
