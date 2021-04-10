using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Covid19Testing.Models;

namespace Covid19Testing.IRepos
{
    public interface IBiodataRepos : IRepository<TblBiodata>
    {
        IEnumerable<TblBiodata> GetAllByName(string name);

        TblBiodata GetByEPIDNo(object id);

        void Delete(int id, string username);

        int TestsCount(int biodata);
    }
}
