using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.OptionsModel;
using PortifolioSite.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PortifolioSite.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private MyConfig Config { get; }
        public HomeController(MyConfig config)
        {
            Config = config;
        }

        // GET: /<controller>/
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            ViewBag.AppName = Config.AppName;
            return View("Index", new Project { Title = "Ray Tracing"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("home/submit"), ActionName("Submit")]
        public IActionResult Submit()
        {
            ViewBag.AppName = Config.AppName;
            return View("Index", new Project { Title = "new project" });
        }
    }
}
