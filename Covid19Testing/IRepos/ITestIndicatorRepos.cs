using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Covid19Testing.Models;

namespace Covid19Testing.IRepos
{
    public interface ITestIndicatorRepos : IRepository<TlkpTestIndicators>
    {
        IEnumerable<TlkpTestIndicators> GetAllByMethod(int id);
    }
}
