using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using PortfolioSite.ViewModels;

namespace PortfolioSite.Controllers
{
    public class HomeController : Controller
    {
        private const string Lorem = "Use this area of the page to describe your project.\n Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";
        public HomeViewModel HomeViewModel { get; set; } = new HomeViewModel
        {
            MainTitle = "Anders Wind",
            HeaderTitle = "Anders Wind's Portfolio",
            HeaderImage = "../images/header1.jpg",
            MainText = "I am a software development student at the IT-University of Copenhagen in Denmark and a freelance developer.",
            ProfileImage = "../images/profile-pic.jpg",
            AboutTitle = "Wind IT",
            AboutText = "My company Wind IT creates software solutions for companies and people, who want to improve they work-processes with IT. I focus on making IT as easy and simple to use - and showing that IT can help in many ways",
            ServicesTitle = "I can help you with",
            ServicesCollection = new List<ServiceViewModel>
            {
                new ServiceViewModel { Title = "Sturdy Templates",  Description = "Our templates are updated regularly so they don't break.",   Symbol = "fa-diamond",      Delay = "0.00s" },
                new ServiceViewModel { Title = "Ready to Ship",     Description = "You can use this theme as is, or you can make changes!",     Symbol = "fa-paper-plane",  Delay = "0.12s" },
                new ServiceViewModel { Title = "Up to Date",        Description = "We update dependencies to keep things fresh.",               Symbol = "fa-newspaper-o",  Delay = "0.24s" },
                new ServiceViewModel { Title = "Made with Love",    Description = "You have to make your websites with love these days!",       Symbol = "fa-heart",        Delay = "0.36s" }
            },
            ProjectCollection = new List<ProjectViewModel>
            {
                //new ProjectViewModel { Category = ".NET",  Title = "Ray Tracing", Image = "https://raw.githubusercontent.com/Awia00/Ray-Tracing/master/RayTracerShowcase.png"},
                new ProjectViewModel { Id="Project1", Category = "Category", Image = "../images/portfolio/1.jpg", Title = "Project 1", Description = Lorem, Client = "StartBootstrap", ClientLink = new Uri("http://startbootstrap.com/"), Service = "Web Development", Date = "February 2016"},
                new ProjectViewModel { Id="Project2", Category = "Category", Image = "../images/portfolio/2.jpg", Title = "Project 2", Description = Lorem, Client = "StartBootstrap", ClientLink = new Uri("http://startbootstrap.com/"), Service = "Web Development", Date = "February 2016"},
                new ProjectViewModel { Id="Project3", Category = "Category", Image = "../images/portfolio/3.jpg", Title = "Project 3", Description = Lorem, Client = "StartBootstrap", ClientLink = new Uri("http://startbootstrap.com/"), Service = "Web Development", Date = "February 2016"},
                new ProjectViewModel { Id="Project4", Category = "Category", Image = "../images/portfolio/4.jpg", Title = "Project 4", Description = Lorem, Client = "StartBootstrap", ClientLink = new Uri("http://startbootstrap.com/"), Service = "Web Development", Date = "February 2016"},
                new ProjectViewModel { Id="Project5", Category = "Category", Image = "../images/portfolio/5.jpg", Title = "Project 5", Description = Lorem, Client = "StartBootstrap", ClientLink = new Uri("http://startbootstrap.com/"), Service = "Web Development", Date = "February 2016"},
                new ProjectViewModel { Id="Project6", Category = "Category", Image = "../images/portfolio/6.jpg", Title = "Project 6", Description = Lorem, Client = "StartBootstrap", ClientLink = new Uri("http://startbootstrap.com/"), Service = "Web Development", Date = "February 2016"}
            },
            SecondaryWebsiteText = "Check out all my projects on Github!",
            SecondaryWebsiteLink = new Uri("https://github.com/Awia00"),
            SecondaryWebsiteImage = "../images/GitHub-Mark-Light-120px-plus.png",
            HaveSecondWebsite = true,
            ContactTitle = "Let's get in touch!",
            ContactText = "Ready to start your next project with me? That's great! Give me a call or send me an email and I will get back to you as soon as possible!",
            CellPhoneNumber = "+45 41 36 48 07",
            Email = "anders-wind@outlook.com"
        };

        public IActionResult Index()
        {
            //todo remove
            //var json = JsonConvert.SerializeObject(HomeViewModel);
            //System.IO.File.WriteAllText(@"C:\Users\ander\Desktop\data.json", json);
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
