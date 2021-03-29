using Covid19Testing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.IRepos
{
    public interface ILabTestRepos: IRepository<LabTestDetailsViewModel>
    {
        //IEnumerable<LabTestDetailsViewModel> GetAllByDateRange(DateTime date1, DateTime date2);
        IEnumerable<LabTestDetailsViewModel> GetAllByDateRange(DateTime date1, DateTime date2);

        IEnumerable<LabTestDetailsViewModel> GetAllByBeforeDate(DateTime dt);

        IEnumerable<LabTestDetailsViewModel> GetAllByAfterDate(DateTime dt);
    }
}
