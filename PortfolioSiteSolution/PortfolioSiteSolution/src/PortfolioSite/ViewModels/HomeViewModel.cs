using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioSite.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public string HeaderTitle { get; set; }
        public string HeaderImage { get; set; }

        public string MainTitle { get; set; }
        public string MainText { get; set; }
        public string ProfileImage { get; set; }

        public string AboutTitle { get; set; }
        public string AboutText { get; set; }

        public string ServicesTitle { get; set; }
        public IEnumerable<ServiceViewModel> ServicesCollection { get; set; }

        public IEnumerable<ProjectViewModel> ProjectCollection { get; set; }

        public bool HaveSecondWebsite { get; set; }
        public string SecondaryWebsiteText { get; set; }
        public string SecondaryWebsiteImage { get; set; }
        public Uri SecondaryWebsiteLink { get; set; }

        public string ContactTitle { get; set; }
        public string ContactText { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string CellPhoneNumber { get; set; }

        public Uri FacebookUri { get; set; }
        public Uri TwitterUri { get; set; }
        public Uri LinkedInUri { get; set; }
        public Uri GooglePlusUri { get; set; }
    }
}
