using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Covid19Testing.Models;
using Covid19Testing.IRepos;
using Covid19Testing.Repos;

namespace Covid19Testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpecimenRepos specimen;
        private readonly ITestIndicatorRepos indicators;
        //private readonly Covid19TestingContext Context;

        public HomeController( ISpecimenRepos _specimen, ITestIndicatorRepos _indicators)
        {
            specimen = _specimen;
            indicators = _indicators;
           // Context = _Context;
        }
        
        public IActionResult Index()
        {
            return View();
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? code)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, HttpCode = code??0 });
        }
    }
}
