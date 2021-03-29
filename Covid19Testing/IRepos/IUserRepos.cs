using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.IRepos
{
    public interface IUserRepos: IRepository<TblUsers>
    {
        // TblUsers GetByUsername(string )
        IEnumerable<TblRoles> GetRoles();

    }
}
