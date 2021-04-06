using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19TestingServer.Models;
using Covid19TestingServer.ViewModels;
using CovidTestingServer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace CovidTestingServer.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class ResultsController : ControllerBase
    {
        private readonly Covid19TestingSrvContext _context;

        public ResultsController(Covid19TestingSrvContext context)
        {
            _context = context;
        }


        // GET: LabTests/Create
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult Add(TblLabTests test)
        {
            //test.MethodNavigation = null;

            //TblBiodata biodata = _context.TblBiodata.FirstOrDefault(b=>b.Id==test.Biodata);

            using (var transaction = _context.Database.BeginTransaction())
            {
                //if (biodata != null)
                //{
                //    //test.BiodataNavigation = null;
                //    //_context.TblBiodata.Add(test.BioData);
                //    //_context.SaveChanges();
                //    //_context.Entry<TblBiodata>(test.BioData).State = EntityState.Detached;
                //}
                //else {
                //    test.BiodataNavigation.GenderNavigation = null;
                //}

                if (_context.TblLabTests.FirstOrDefault(t => t.Id == test.Id) != null)
                {
                    return NotFound($"Covid19 test result already published on server.");
                }

                test.BiodataNavigation.GenderNavigation = null;
                test.MethodNavigation = null;

                _context.TblLabTests.Add(test);
                _context.SaveChanges();
                transaction.Commit();
                string uri = string.Format("{0}://{1}{2}/{3}", HttpContext.Request.Scheme, HttpContext.Request.Host, HttpContext.Request.Path, test.Id);
                return Created(uri, test);



            }


            //return NotFound($"Covid19 test result not added on purpose.");
        }

        // GET: LabTests/Create
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult get(int id)
        {
            /*TblBiodata biodata = _context.TblBiodata
                                .FirstOrDefault(x => x.EpidNo == epidno);
            if (biodata == null)
            {
                return NotFound($"No Candidate exists with EPID-NO: {epidno}");
            }*/

            TblLabTests test = _context.TblLabTests
                .Include(x => x.BiodataNavigation)
                .Include(x => x.TblLabTestsIndicatorsValues)
                .Include(x=> x.TblLabTestsSpecimen)
                //.OrderByDescending(x => x.TestingDate)
                //.OrderByDescending(x=> x.TestingTime)
                .FirstOrDefault(x=>x.Id==id);

            //TblBiodata biodata = _context.TblBiodata
            //    .in
            //    .FirstOrDefault(x => x.EpidNo == epidno);

            if (test != null)
            {
                return Ok(test);
            }

            return NotFound($"No Covid19 test result found.");
        }
    }
}