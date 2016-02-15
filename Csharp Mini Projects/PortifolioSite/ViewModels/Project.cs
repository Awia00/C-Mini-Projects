using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortifolioSite.ViewModels
{
    public class Project
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
    }
}
