using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.IRepos
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Create(T obj);
        void Update(T obj);
        void Delete(object id);
        void Archive(object id);
        void Save(T obj);

    }
}
