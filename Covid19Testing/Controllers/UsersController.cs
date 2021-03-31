using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Covid19Testing.Models;
using Covid19Testing.IRepos;
using Microsoft.AspNetCore.Authorization;

namespace Covid19Testing.Controllers
{
    [Authorize(Roles = "administrator")]
    public class UsersController : Controller
    {
        private readonly IUserRepos users;

        public UsersController(IUserRepos _users)
        {
            //_context = context;
            users = _users;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            //var covid19TestingContext = _context.TblUsers.Include(t => t.UserroleNavigation);
            //return View(await covid19TestingContext.ToListAsync());
            return View(users.GetAll());
        }

        // GET: Biodata/Details/5
        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var _user = users.GetById(id);
            
            if (_user == null)
            {
                return NotFound();
            }

            return View(_user);
        }

        // GET: Biodata/Edit/5
        [Route("Edit")]
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var user = users.GetById(id);

            if(User.Identity.Name==user.Username)
                return RedirectToAction(nameof(Index));

            if (user == null)
            {
                return NotFound();
            }

            ViewData["Userrole"] = new SelectList(users.GetRoles(), "Id", "Rolename", user.Userrole);

            return View(user);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public IActionResult Edit(int id, TblUsers _user)
        {
            if (id != _user.Id)
            {
                return NotFound();
            }
            //_user.Id = 10; //Dummy number

            //var errors = ModelState.Values;
            //foreach (var e in errors)
            //{
            //    ;
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _user.UpdateBy = User.Identity.Name;
                    users.Update(_user);
                    /*_context.Update(tblBiodata);
                    await _context.SaveChangesAsync();*/
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblUsersExists(_user.Username))
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
                    //ViewData["Error"] = "Duplicate EPID-NO";
                }
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["Gender"] = new SelectList(/*_context.TlkpGenders*/ genders.GetAll(), "Id", "Gender");
            ViewData["Userrole"] = new SelectList(users.GetRoles(), "Id", "Rolename", _user.Userrole);
            return View(_user);
        }




            private bool TblUsersExists(string id)
        {
            //return _context.TblUsers.Any(e => e.Username == id);
            return users.GetById(id) != null;
        }
    }
}
