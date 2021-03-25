using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Covid19Testing.IRepos;
using Covid19Testing.Models;

namespace Covid19Testing.Repos
{
    public class TestIndicatorRepos : ITestIndicatorRepos
    {
        public TestIndicatorRepos(Covid19TestingContext _Context)
        {
            Context = _Context;// new Covid19TestingContext();
        }
        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TlkpTestIndicators obj)
        {
            //throw new NotImplementedException();
            Context.TlkpTestIndicators.Add(obj);
        }

        public void Delete(object id)
        {
            //throw new NotImplementedException();
            TlkpTestIndicators indicator = GetById(id);
            if (indicator != null)
            {
                Context.TlkpTestIndicators.Remove(indicator);
                //Save();
                Context.SaveChanges();
            }
        }

        public IEnumerable<TlkpTestIndicators> GetAll()
        {
            //throw new NotImplementedException();
            return Context.TlkpTestIndicators;
        }

        public IEnumerable<TlkpTestIndicators> GetAllByMethod(int id)
        {
            //throw new NotImplementedException();
            return Context.TlkpTestIndicators.Where(m => m.Method == id);

        }

        public TlkpTestIndicators GetById(object id)
        {
            //throw new NotImplementedException();
            return Context.TlkpTestIndicators.Find(id);
        }

        public void Save(TlkpTestIndicators obj)
        {
            //throw new NotImplementedException();
            Context.SaveChanges();
            Context.Entry(obj).ReloadAsync();
        }

        public void Update(TlkpTestIndicators obj)
        {
            //throw new NotImplementedException();
            TlkpTestIndicators indicator = GetById(obj.Id);
            if (indicator != null)
            {
                indicator.IndicatorName = obj.IndicatorName;
                Save(indicator);
            }
        }
    }
}
