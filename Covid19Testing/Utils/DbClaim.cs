using Covid19Testing.Models;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Covid19Testing.Utils
{
    public class DbClaim : IClaimsTransformation
    {
        public DbClaim(Covid19TestingContext _Context)
        {
            Context = _Context;// new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            //throw new NotImplementedException();
            var clone = principal.Clone();
            var identity = (ClaimsIdentity)clone.Identity;
            var userName = identity.Name;
            
            var user = Context.TblUsers
                .FirstOrDefault(u => u.Username == userName);

            if (user == null) {
                user = new TblUsers { Username = userName, UpdateBy= userName };
                //user.InsertTime = DateTime.Now;
                Context.TblUsers.Add(user);
                Context.SaveChanges();

            }

            var role = user==null?0:user.Userrole;

            var roles = Context.TblRoles;

            foreach (var r in roles)
            {
                if (role >= r.Id)
                {
                    var claim = new Claim(identity.RoleClaimType, r.Rolename);
                    identity.AddClaim(claim);
                }
                
            }
             return Task.FromResult(clone);
        }
    }
}
