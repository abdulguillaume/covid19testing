using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19TestingServer.Models;
using Microsoft.AspNetCore.Http;

namespace CovidTestingServer.Controllers
{
    public class LabTestsController : Controller
    {
        private readonly Covid19TestingSrvContext _context;

        Dictionary<int, string> specimen = new Dictionary<int, string>();

        Dictionary<int, string> indicators = new Dictionary<int, string>();

        public LabTestsController(Covid19TestingSrvContext context)
        {
            _context = context;

            foreach (var s in _context.TlkpSpecimen)
            {
                specimen[s.Id] = s.Type;
            }

            foreach (var i in _context.TlkpTestIndicators)
            {
                indicators[i.Id] = i.IndicatorName;
            }

        }

        // GET: LabTests
        public async Task<IActionResult> Index()
        {
            //var covid19TestingSrvContext = _context.TblLabTests.Include(t => t.BiodataNavigation).Include(t => t.MethodNavigation);
            //_context.TblLabTests;

            string _keyword = HttpContext.Session.GetString("testsrv_search_keyword");
            string _date1 = HttpContext.Session.GetString("testsrv_search_date1");

            if (!string.IsNullOrEmpty(_keyword) && !string.IsNullOrEmpty(_date1))
            {

                //return search(_page, _pageSize);
                return RedirectToAction(nameof(search), new {  keyword = _keyword, date1 = _date1 });
            }

            ViewBag.NoResult = true;

            return View(
                            await _context.TblLabTests
                            .Where(x=>x.Id==-1)
                            //.Include(x=>x.BiodataNavigation)
                            .ToListAsync()
                        );
        }


        [HttpGet]
        [Route("Labtests/search")]
        public async Task<IActionResult> search()
        {

            var keyword = Request.Query["keyword"].ToString();

            ViewBag.keyword = keyword;

            var date1 = Request.Query["date1"].ToString();
            
            ViewBag.date1 = date1;

            HttpContext.Session.SetString("testsrv_search_keyword", keyword);
            HttpContext.Session.SetString("testsrv_search_date1", date1);

            return View("Index",
                await _context.TblLabTests
                            .Include(x => x.BiodataNavigation)
                            //.Include(x=> x.TblLabTestsIndicatorsValues)
                            //.Include(x=>x.TblLabTestsSpecimen)
                            .Where(x=>x.BiodataNavigation.EpidNo==keyword && x.TestingDate.Value.Date==DateTime.Parse(date1).Date)
                            .ToListAsync()
                        );

            //if(string.IsNullOrEmpty(date1))


            //if (string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            //    model = new PagedList<LabTestDetailsViewModel>(tests.GetAllByBeforeDate(DateTime.Parse(date2)).AsQueryable(), page, pageSize);
            //else if (string.IsNullOrEmpty(date2) && !string.IsNullOrEmpty(date1))
            //    model = new PagedList<LabTestDetailsViewModel>(tests.GetAllByAfterDate(DateTime.Parse(date1)).AsQueryable(), page, pageSize);
            //else if (!string.IsNullOrEmpty(date2) && !string.IsNullOrEmpty(date1))
            //    model = new PagedList<LabTestDetailsViewModel>(tests.GetAllByDateRange(DateTime.Parse(date1), DateTime.Parse(date2)).AsQueryable(), page, pageSize);
            //else
            //    model = new PagedList<LabTestDetailsViewModel>(tests.GetAll().AsQueryable(), page, pageSize); //ok

            //ViewBag.keyword = keyword;
            //return View("Index");//, model);
            //return null;
        }
        // GET: LabTests/Details/5
        public async Task<IActionResult> Details(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var tblLabTests = await _context.TblLabTests
                            .Include(t => t.MethodNavigation)
                            .Include(x => x.BiodataNavigation)
                            .Include(x => x.TblLabTestsIndicatorsValues)
                            .Include(x => x.TblLabTestsSpecimen)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblLabTests == null)
            {
                return NotFound();
            }

            ViewBag.specimen = specimen;
            ViewBag.indicators = indicators;

            return View(tblLabTests);
        }
    }
}
