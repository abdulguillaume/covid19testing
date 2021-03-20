using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Testing.Models;
using Covid19Testing.IRepos;
using PagedList.Core;
using Covid19Testing.ViewModels;

namespace Covid19Testing.Controllers
{
    public class LabTestsController : Controller
    {
        //private readonly Covid19TestingContext _context;

        private readonly ILabTestRepos tests;
        private readonly IBiodataRepos biodata;
        private readonly IMethodRepos methods;

        //private readonly ITestIndicatorRepos indicators;
        private readonly ISpecimenRepos specimen;

        const int _pageSize = 20;

        public LabTestsController(ILabTestRepos _tests, IBiodataRepos _biodata, IMethodRepos _methods)//Covid19TestingContext context)
        {
            tests = _tests;
            biodata = _biodata;
            methods = _methods;
        }

        // GET: LabTests
        public async Task<IActionResult> Index()
        {
            //var covid19TestingContext = _context.TblLabTests.Include(t => t.BiodataNavigation).Include(t => t.MethodNavigation);

            //return View(await covid19TestingContext.ToListAsync());

            PagedList<LabTestDetailsViewModel> model = new PagedList<LabTestDetailsViewModel>(tests.GetAll().AsQueryable(), 1, _pageSize);

            return View(model);
        }

        // GET: LabTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LabTest = tests.GetById(id);/*await _context.TblLabTests
                .Include(t => t.BiodataNavigation)
                .Include(t => t.MethodNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);*/
            if (LabTest == null)
            {
                return NotFound();
            }

            return View(LabTest);
        }

        // GET: LabTests/Create
        public IActionResult Create(int _bio)
        {
            TblBiodata _Biodata = biodata.GetById(_bio);

            LabTestDetailsViewModel _labTest = new LabTestDetailsViewModel(_Biodata, methods, specimen);

            ViewData["Method"] = new SelectList(methods.GetAll(), "Id", "Methodname");

            /* ViewData["Biodata"] = new SelectList(_context.TblBiodata, "Id", "Fullname");
             ViewData["Method"] = new SelectList(_context.TlkpTestMethods, "Id", "InsertBy");*/
            return View();
        }

        // POST: LabTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabTestDetailsViewModel _labTest)//[Bind("Id,Biodata,Method,Interpretation,TestingDate,TestingTime,ReportingDate,ReportingTime,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblLabTests tblLabTests)
        {
            if (ModelState.IsValid)
            {
                tests.Create(_labTest);
                //_context.Add(tblLabTests);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Method"] = new SelectList(methods.GetAll(), "Id", "Methodname");
            /*ViewData["Biodata"] = new SelectList(_context.TblBiodata, "Id", "Fullname", tblLabTests.Biodata);
            ViewData["Method"] = new SelectList(_context.TlkpTestMethods, "Id", "InsertBy", tblLabTests.Method);*/
            return View(_labTest);
        }

        // GET: LabTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return NotFound();
            /*if (id == null)
            {
                return NotFound();
            }

            var tblLabTests = await _context.TblLabTests.FindAsync(id);
            if (tblLabTests == null)
            {
                return NotFound();
            }
            ViewData["Biodata"] = new SelectList(_context.TblBiodata, "Id", "Fullname", tblLabTests.Biodata);
            ViewData["Method"] = new SelectList(_context.TlkpTestMethods, "Id", "InsertBy", tblLabTests.Method);
            return View(tblLabTests);*/
        }

        // POST: LabTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Biodata,Method,Interpretation,TestingDate,TestingTime,ReportingDate,ReportingTime,InsertTime,InsertBy,UpdateTime,UpdateBy")] TblLabTests tblLabTests)
        {
            return NotFound();
            /*
            if (id != tblLabTests.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblLabTests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblLabTestsExists(tblLabTests.Id))
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
            ViewData["Biodata"] = new SelectList(_context.TblBiodata, "Id", "Fullname", tblLabTests.Biodata);
            ViewData["Method"] = new SelectList(_context.TlkpTestMethods, "Id", "InsertBy", tblLabTests.Method);
            return View(tblLabTests);*/
        }

        // GET: LabTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return NotFound();
            /*if (id == null)
            {
                return NotFound();
            }

            var tblLabTests = await _context.TblLabTests
                .Include(t => t.BiodataNavigation)
                .Include(t => t.MethodNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblLabTests == null)
            {
                return NotFound();
            }

            return View(tblLabTests);*/
        }

        // POST: LabTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*var tblLabTests = await _context.TblLabTests.FindAsync(id);
            _context.TblLabTests.Remove(tblLabTests);
            await _context.SaveChangesAsync();*/
            return RedirectToAction(nameof(Index));
        }

        private bool TblLabTestsExists(int id)
        {
            return tests.GetById(id) != null;
        }
    }
}
