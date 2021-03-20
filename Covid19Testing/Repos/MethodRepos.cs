using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Repos
{
    public class MethodRepos : IMethodRepos
    {
        public MethodRepos()
        {
            Context = new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TlkpTestMethods obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TlkpTestMethods> GetAll()
        {
            //throw new NotImplementedException();
            return Context.TlkpTestMethods;
        }

        public TlkpTestMethods GetById(object id)
        {
            //throw new NotImplementedException();
            return Context.TlkpTestMethods.Find(id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(TlkpTestMethods obj)
        {
            throw new NotImplementedException();
        }
    }
}
