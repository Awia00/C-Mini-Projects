using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioSite.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public string Title { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        //todo make own data anootation requirement 0.1s for example
        public string Delay { get; set; }
    }
}
