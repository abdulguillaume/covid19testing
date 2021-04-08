using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Covid19Testing.Models;
using Covid19Testing.IRepos;
using Covid19Testing.Repos;
using Covid19Testing.ViewModels;

namespace Covid19Testing.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ISpecimenRepos specimen;
        //private readonly ITestIndicatorRepos indicators;
        //private readonly Covid19TestingContext Context;

        private readonly ILabTestRepos tests;

        public HomeController(ILabTestRepos _tests )//ISpecimenRepos _specimen, ITestIndicatorRepos _indicators)
        {
            //specimen = _specimen;
            //indicators = _indicators;
            // Context = _Context;
            tests = _tests;
        }
        
        public IActionResult Index()
        {
            DateTime dt = DateTime.Now;

            DateTime dt72hrs = dt.AddDays(-3);

            @ViewBag.date1 = dt72hrs.ToString("yyyy-MM-dd");

            Rpt1ViewModel rpt = tests.GetRpt(dt72hrs.Date);

            return View("Index", rpt);
        }

        [HttpGet]
        [Route("Home/search")]
        public IActionResult search()
        {
            var date1 = Request.Query["date1"].ToString();

            if (string.IsNullOrEmpty(date1) || DateTime.Parse(date1)>DateTime.Now)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.date1 = date1;

            DateTime dt = DateTime.Parse(date1);

            Rpt1ViewModel rpt = tests.GetRpt(dt.Date);

            return View("Index", rpt);
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
