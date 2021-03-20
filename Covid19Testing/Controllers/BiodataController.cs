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

namespace Covid19Testing.Controllers
{
    public class BiodataController : Controller
    {
        //private readonly Covid19TestingContext _context;
        private readonly IBiodataRepos biodata;

        readonly GenderRepos genders = new GenderRepos();

        const int _pageSize = 20;

        public BiodataController(IBiodataRepos _biodata)//Covid19TestingContext context)
        {
            //_context = context;
            biodata = _biodata;
        }

        [HttpGet]
        [Route("search")]
        public IActionResult search(int page = 1, int pageSize = _pageSize)
        {
            var keyword = Request.Query["keyword"].ToString();
            PagedList<TblBiodata> model = new PagedList<TblBiodata>(biodata.GetAllByName(keyword).AsQueryable(), page, pageSize);
            ViewBag.keyword = keyword;
            return View("Index", model);
        }

        // GET: Biodata
        public async Task<IActionResult> Index()
        {
            //var covid19TestingContext = _context.TblBiodata.Include(t => t.GenderNavigation);


            //return View(await covid19TestingContext.ToListAsync());
            PagedList<TblBiodata> model = new PagedList<TblBiodata>(biodata.GetAll().AsQueryable(), 1, _pageSize);
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

            /*var tblBiodata = await _context.TblBiodata
                .Include(t => t.GenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);*/
            if (tblBiodata == null)
            {
                return NotFound();
            }

            return View(tblBiodata);
        }

        // GET: Biodata/Create
        public IActionResult Create()
        {
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            return View();
        }

        // POST: Biodata/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,LegalGardianName,Dateofbirth,Gender,EpidNo,HomePhone,ResidentialAddress,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblBiodata tblBiodata)
        {
            if (ModelState.IsValid)
            {
                biodata.Create(tblBiodata);
                /*_context.Add(tblBiodata);
                await _context.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            return View(tblBiodata);
        }

        // GET: Biodata/Edit/5
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
            ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            return View(tblBiodata);
        }

        // POST: Biodata/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fullname,LegalGardianName,Dateofbirth,Gender,EpidNo,HomePhone,ResidentialAddress,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblBiodata tblBiodata)
        {
            if (id != tblBiodata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    biodata.Update(tblBiodata);
                    /*_context.Update(tblBiodata);
                    await _context.SaveChangesAsync();*/
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
                return RedirectToAction(nameof(Index));
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
