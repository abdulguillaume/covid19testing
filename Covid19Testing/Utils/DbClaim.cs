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
        public DbClaim()
        {
            Context = new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            //throw new NotImplementedException();
            var identity = (ClaimsIdentity)principal.Identity;
            var userName = identity.Name;
            var user = Context.TblUsers
                .FirstOrDefault(u => u.Username == userName);
            var role = user==null?0:user.Userrole;

            var roles = Context.TblRoles;

            foreach (var r in roles)
            {
                if (role >= r.Id)
                {
                    var claim = new Claim(ClaimTypes.Role, r.Rolename);
                    identity.AddClaim(claim);
                }
                
            }
            return Task.FromResult(principal);
        }
    }
}
