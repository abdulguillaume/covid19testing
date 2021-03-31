using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.IRepos
{
    public interface IMailingRepos : IRepository<TblMailingLists>
    {
        IEnumerable<TlkpMailingGroups> GetAllGrps();
        Dictionary<int, string> GetAllGrpsDict();
        TlkpMailingGroups GetGrpByName(string grpname);
        bool CreateGrp(TlkpMailingGroups grp);
        void DeleteGrp(TlkpMailingGroups grp);
        void MapToGrp(TblMailingLists mail, TlkpMailingGroups grp);
        void UnmapToGrp(TblMailingLists mail, TlkpMailingGroups grp);
    }
}
