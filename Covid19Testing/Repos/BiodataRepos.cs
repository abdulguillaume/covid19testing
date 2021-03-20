using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Repos
{
    public class BiodataRepos : IBiodataRepos
    {
        GenderRepos genders = new GenderRepos();

        string admin = "admin";

        public BiodataRepos()
        {
            Context = new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TblBiodata obj)
        {
            //throw new NotImplementedException();
            
            obj.InsertBy=
            obj.UpdateBy = admin;

            Context.TblBiodata.Add(obj);

            obj.InsertTime =
            obj.UpdateTime = DateTime.Now;

            Save();
        }

        public void Delete(object id)
        {
            //throw new NotImplementedException();
            TblBiodata subject = GetById(id);

            if (subject != null)
            {
                Context.TblBiodata.Remove(subject);
                //Save();
            }
        }

        public IEnumerable<TblBiodata> GetAll()
        {
            return Context.TblBiodata;
        }

        public IEnumerable<TblBiodata> GetAllByName(string name)
        {
            //throw new NotImplementedException();
            return Context.TblBiodata.Where(x => x.Fullname.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public TblBiodata GetByEPIDNo(object id)
        {
            return Context.TblBiodata.FirstOrDefault(x=>((string)id).CompareTo(x.EpidNo)==0);
        }

        public TblBiodata GetById(object id)
        {
            return Context.TblBiodata.Find(id);
        }

        public void Save()
        {
            //throw new NotImplementedException();
            Context.SaveChanges();
        }

        public void Update(TblBiodata obj)
        {
            //throw new NotImplementedException();
            TblBiodata subject = GetById(obj.Id);

            if (subject != null)
            {
                subject.Fullname = obj.Fullname;
                subject.LegalGardianName = obj.LegalGardianName;
                subject.Dateofbirth = DateTime.Parse(obj.Dateofbirth.ToShortDateString());
                subject.EpidNo = obj.EpidNo;
                subject.HomePhone = obj.HomePhone;
                subject.ResidentialAddress = obj.ResidentialAddress;
                subject.UpdateBy = admin;
                subject.UpdateTime = DateTime.Now;

                Save();
            }
        }
    }
}
