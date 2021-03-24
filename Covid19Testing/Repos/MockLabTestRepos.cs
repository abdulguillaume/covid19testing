using Covid19Testing.IRepos;
using Covid19Testing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Repos
{
    public class MockLabTestRepos : ILabTestRepos
    {
        //List<LabTestDetailsViewModel> _labtests;


        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(LabTestDetailsViewModel obj)
        {
            throw new NotImplementedException();

        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LabTestDetailsViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public LabTestDetailsViewModel GetById(object id)
        {
            throw new NotImplementedException();
            
        }

        public void Save(LabTestDetailsViewModel obj)
        {
            throw new NotImplementedException();
        }

        public void Update(LabTestDetailsViewModel obj)
        {
            throw new NotImplementedException();
            
        }
    }
}
