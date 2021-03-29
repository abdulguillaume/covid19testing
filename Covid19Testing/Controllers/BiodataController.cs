using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Testing.Models;
using Covid19Testing.IRepos;
using Covid19Testing.Repos;
using PagedList.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Covid19Testing.Controllers
{
    //[Authorize(Roles = "dataentry5")]
    public class BiodataController : Controller
    {
        //private readonly Covid19TestingContext _context;
        private readonly IBiodataRepos biodata;

        readonly IGenderRepos genders;//= new GenderRepos();

        const int _pageSize = 3;

        public BiodataController(IGenderRepos _genders,IBiodataRepos _biodata)//Covid19TestingContext context)
        {
            //_context = context;
            biodata = _biodata;
            genders = _genders;
        }

        [HttpGet]
        [Route("Biodata/search")]
        public IActionResult search(int page = 1, int pageSize = _pageSize)
        {
            HttpContext.Session.SetString("biodata_details_token", "");
            var keyword = Request.Query["keyword"].ToString();
            HttpContext.Session.SetString("biodata_search_keyword", keyword);

            HttpContext.Session.SetInt32("biodata_search_page",  page);
            PagedList<TblBiodata> model = new PagedList<TblBiodata>(biodata.GetAllByName(keyword).AsQueryable(), page, pageSize);
            ViewBag.keyword = keyword;
            return View("Index", model);
        }

        // GET: Biodata
        public async Task<IActionResult> Index()
        {
            //var covid19TestingContext = _context.TblBiodata.Include(t => t.GenderNavigation);
            HttpContext.Session.SetString("biodata_details_token", "");

            string _keyword = HttpContext.Session.GetString("biodata_search_keyword");

            int _page = 1;
            try
            {
                _page = HttpContext.Session.GetInt32("biodata_search_page").Value;
            }
            catch { }

            if (!string.IsNullOrEmpty(_keyword)) {
                
                //return search(_page, _pageSize);
                return RedirectToAction(nameof(search), new { page = _page, keyword = _keyword });
            }
            //return View(await covid19TestingContext.ToListAsync());
            PagedList<TblBiodata> model = new PagedList<TblBiodata>(biodata.GetAll().AsQueryable(), _page, _pageSize);
            return View(model);
        }


        // GET: Biodata/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tblBiodata = biodata.GetById(id);

            HttpContext.Session.SetString("biodata_details_token", "biodata_details_token");


            /*var tblBiodata = await _context.TblBiodata
                .Include(t => t.GenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);*/
            if (tblBiodata == null)
            {
                return NotFound();
            }

            return View(tblBiodata);
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        // GET: Biodata/Create
        [Authorize]
        public IActionResult Create()
        {
            HttpContext.Session.SetString("biodata_details_token", "");
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            ViewData["Random"] = "Temp-"+GenerateRandomNo().ToString();
            return View();
        }

        // POST: Biodata/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,LegalGardianName,Dateofbirth,Gender,EpidNo,LocalPhone,HomePhone,ResidentialAddress,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblBiodata tblBiodata)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tblBiodata.InsertBy = tblBiodata.UpdateBy = User.Identity.Name;
                    biodata.Create(tblBiodata);
                    /*_context.Add(tblBiodata);
                    await _context.SaveChangesAsync();*/
                    return RedirectToAction(nameof(Index));
                }

            }
            catch {
                ViewData["Error"] = "Duplicate EPID-NO";
                ViewData["Random"] = tblBiodata.EpidNo;
            }
            
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            return View(tblBiodata);
        }

        // GET: Biodata/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblBiodata = biodata.GetById(id);//await _context.TblBiodata.FindAsync(id);
            if (tblBiodata == null)
            {
                return NotFound();
            }
            HttpContext.Session.SetString("biodata_details_token", "");
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            return View(tblBiodata);
        }

        // POST: Biodata/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fullname,LegalGardianName,Dateofbirth,Gender,EpidNo,HomePhone,LocalPhone,ResidentialAddress,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblBiodata tblBiodata)
        {
            if (id != tblBiodata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblBiodata.UpdateBy = User.Identity.Name;
                    biodata.Update(tblBiodata);
                    /*_context.Update(tblBiodata);
                    await _context.SaveChangesAsync();*/
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBiodataExists(tblBiodata.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch
                {
                    ViewData["Error"] = "Duplicate EPID-NO";
                }
                //return RedirectToAction(nameof(Index));
            }
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            return View(tblBiodata);
        }

        // GET: Biodata/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tblBiodata = biodata.GetById(id);
            /*var tblBiodata = await _context.TblBiodata
                .Include(t => t.GenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);*/
            HttpContext.Session.SetString("biodata_details_token", "");

            if (tblBiodata == null)
            {
                return NotFound();
            }

            return View(tblBiodata);
        }

        // POST: Biodata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var tblBiodata = await _context.TblBiodata.FindAsync(id);
            _context.TblBiodata.Remove(tblBiodata);
            await _context.SaveChangesAsync();*/
            var tblBiodata = biodata.GetById(id);
            biodata.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TblBiodataExists(int id)
        {
            return biodata.GetById(id)!=null;
            //return _context.TblBiodata.Any(e => e.Id == id);
        }
    }
}
