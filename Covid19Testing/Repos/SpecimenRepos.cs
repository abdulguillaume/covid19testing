using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Repos
{
    public class SpecimenRepos : ISpecimenRepos
    {
        public SpecimenRepos()
        {
            Context = new Covid19TestingContext(); 
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TlkpSpecimen obj)
        {
            //throw new NotImplementedException();
            Context.TlkpSpecimen.Add(obj);
        }

        public void Delete(object id)
        {
            //throw new NotImplementedException();
            TlkpSpecimen specimen = GetById(id);
            if (specimen != null)
            {
                Context.TlkpSpecimen.Remove(specimen);
                Save();
            }
        }

        public IEnumerable<TlkpSpecimen> GetAll()
        {
            //throw new NotImplementedException();
            return Context.TlkpSpecimen;
        }

        public TlkpSpecimen GetById(object id)
        {
            //throw new NotImplementedException();
            return Context.TlkpSpecimen.Find(id);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            Context.SaveChanges();
        }

        public void Update(TlkpSpecimen obj)
        {
            //throw new NotImplementedException();
            TlkpSpecimen specimen = GetById(obj.Id);
            if (specimen != null)
            {
                specimen.Type = obj.Type;
                Save();
            }
        }
    }
}
