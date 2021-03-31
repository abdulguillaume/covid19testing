using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Testing.Models;
using Microsoft.AspNetCore.Authorization;
using Covid19Testing.IRepos;
using Covid19Testing.Utils;
using Newtonsoft.Json;

namespace Covid19Testing.Controllers
{
    [Authorize(Roles = "dataentry10")]
    public class MailingListsController : Controller
    {
        private readonly IMailingRepos mailing;

        public MailingListsController(IMailingRepos _mailing)
        {
            mailing = _mailing;
        }

        // GET: MailingLists
        public async Task<IActionResult> Index()
        {
            ViewData["Groups"] = mailing.GetAllGrpsDict();

            return View(mailing.GetAll());
        }

        // GET: MailingLists/Create
        public IActionResult Create()
        {
            ViewData["Groups"] = new SelectList(mailing.GetAllGrps(), "Id", "GroupName");
            return View();
        }

        // POST: MailingLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblMailingLists tblMailingLists, List<int> selected_groups)
        {
            if (ModelState.IsValid)
            {

                if (selected_groups.Count == 0)
                {
                    selected_groups.Add(1);
                }

                foreach(var g in selected_groups) {
                    TblEmailGroupMapping mapping = new TblEmailGroupMapping();
                    mapping.MailingGroup = g;
                    tblMailingLists.TblEmailGroupMapping.Add(mapping);
                }

                tblMailingLists.InsertBy = User.Identity.Name;

                mailing.Create(tblMailingLists);
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["Groups"] = new SelectList(mailing.GetAllGrps(), "Id", "GroupName");

            return View(tblMailingLists);
        }

        // GET: MailingLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMailingLists = mailing.GetById(id);

            if (tblMailingLists == null)
            {
                return NotFound();
            }

            ViewData["Groups"] = mailing.GetAllGrpsDict();

            return View(tblMailingLists);
        }

        // POST: MailingLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TblMailingLists tblMailingLists, List<int> selected_groups)
        {
            if (id != tblMailingLists.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (selected_groups.Count == 0)
                {
                    selected_groups.Add(1);
                }

                try
                {
                    var _tblMailingLists = mailing.GetById(tblMailingLists.Id);

                    
                    if (_tblMailingLists==null)
                    {
                        return NotFound();
                    }

                    foreach (var g in selected_groups) {
                        tblMailingLists.TblEmailGroupMapping.Add(new TblEmailGroupMapping { MailingGroup=g, Email= _tblMailingLists.Id});
                    }

                    mailing.Update(tblMailingLists);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMailingListsExists(tblMailingLists.Id))
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

            ViewData["Groups"] = new SelectList(mailing.GetAllGrps(), "Id", "GroupName");

            return View(tblMailingLists);
        }

        // GET: MailingLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMailingLists = mailing.GetById(id);

            if (tblMailingLists == null)
            {
                return NotFound();
            }

            ViewData["Groups"] = mailing.GetAllGrpsDict();

            return View(tblMailingLists);
        }

        // POST: MailingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMailingLists = mailing.GetById(id);
            if (tblMailingLists != null)
            {
                    mailing.Delete(id);

            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult CreateGroup(string _group)
        {
            ActionResponse response = new ActionResponse { };

            try
            {
                if (string.IsNullOrEmpty(_group))
                {
                    //response.IsSuccessful = false;
                    response.Message = "Group Mailing Name cannot be empty.";
                    throw new Exception();
                }

                TlkpMailingGroups t = new TlkpMailingGroups { GroupName = _group };

                if (mailing.CreateGrp(t))
                {
                    response.IsSuccessful = true;
                    response.Message = "n/a";

                }
                else {

                    response.IsSuccessful = false;
                    response.Message = "Group already exists!";

                }

                //response.JSONData = JsonConvert.SerializeObject("Ok");

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                if(string.IsNullOrEmpty(response.Message))
                    response.Message = "Unknown error, please contact your admin.";
            }
            return Json(response, new JsonSerializerSettings());
        }

        private bool TblMailingListsExists(int id)
        {
            return mailing.GetById(id) != null;

        }
    }
}
