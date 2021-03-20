using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Covid19Testing.IRepos;
using Covid19Testing.Models;

namespace Covid19Testing.Repos
{
    public class GenderRepos : IGenderRepos
    {
        public GenderRepos()
        {
            Context = new Covid19TestingContext();
        }
        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TlkpGenders obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TlkpGenders> GetAll()
        {
            //throw new NotImplementedException();
            return Context.TlkpGenders;
        }


        public TlkpGenders GetById(object id)
        {
            //throw new NotImplementedException();
            return Context.TlkpGenders.Find(id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TlkpGenders obj)
        {
            throw new NotImplementedException();
        }
    }
}
