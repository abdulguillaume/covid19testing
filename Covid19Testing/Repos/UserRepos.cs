using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Covid19Testing.Repos
{
    public class UserRepos : IUserRepos
    {
        public UserRepos(Covid19TestingContext _Context)
        {
            Context = _Context;// new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TblUsers obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TblUsers> GetAll()
        {
            //throw new NotImplementedException();
            return Context.TblUsers
                .Include(u => u.UserroleNavigation);
        }

        public TblUsers GetById(object id)
        {
            //throw new NotImplementedException();
            return Context.TblUsers
                .Include(u=>u.UserroleNavigation)
                .FirstOrDefault(u=>u.Id==(int)id);
        }

        public IEnumerable<TblRoles> GetRoles()
        {
            return Context.TblRoles;
        }

        public void Save(TblUsers obj)
        {
            Context.SaveChanges();
            Context.Entry(obj).ReloadAsync();
        }

        public void Update(TblUsers obj)
        {
            //throw new NotImplementedException();

            TblUsers user = GetById(obj.Id);

            if (user != null)
            {
                user.UpdateBy = obj.UpdateBy;

                if (obj.locked && !user.locked)
                {
                    //user.LockTime = DateTime.Now;
                    user.LockedBy = obj.UpdateBy;
                    user.LockTime = obj.LockTime;
                }
                else if (!obj.locked && user.locked) {
                    user.LastunlockedBy = obj.UpdateBy;
                    //user.LastunlockTime = obj.UpdateTime.Value;
                    user.LockedBy = null;
                    user.LockTime = null;
                }

                user.Userrole = obj.Userrole;

                Save(user);
            }
        }
    }
}
