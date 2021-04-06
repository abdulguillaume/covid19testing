using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Covid19TestingServer.Models;

namespace Covid19TestingServer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }// Covid19TestingSrvContext _Context)
        //{
        //    //Context = _Context;// new Covid19TestingContext();
        //}

        //public Covid19TestingSrvContext Context { get; }


        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "LabTests");
            //return View();
        }

       /* public IActionResult About()
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
