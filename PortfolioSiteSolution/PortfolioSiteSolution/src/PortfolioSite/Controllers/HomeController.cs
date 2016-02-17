using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using PortfolioSite.ViewModels;

namespace PortfolioSite.Controllers
{
    public class HomeController : Controller
    {
        public HomeViewModel HomeViewModel { get; set; } = new HomeViewModel
        {
            MainTitle = "Anders Wind",
            HeaderTitle = "Anders Wind's Portfolio",
            MainText = "I am a software development student at the IT-University of Copenhagen in Denmark and a freelance developer.",
            ProfilePicLink = "~/images/profile-pic.jpg",
            AboutTitle = "Wind IT",
            AboutText = "My company Wind IT creates software solutions for companies and people, who want to improve they work-processes with IT. I focus on making IT as easy and simple to use - and showing that IT can help in many ways",
            ServicesTitle = "I can help you with",
            ServicesCollection = new List<Tuple<string, Uri>> { new Tuple<string, Uri>(".NET",new Uri("http://www.google.dk")) },
            ProjectCollection = new List<ProjectViewModel>(),
            ContactTitle = "Let's get in touch!",
            ContactText = "Ready to start your next project with me? That's great! Give me a call or send me an email and I will get back to you as soon as possible!",
            CellPhoneNumber = "+45 41 36 48 07",
            Email = "anders-wind@outlook.com"
        };

        public IActionResult Index()
        {
            return View(HomeViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
