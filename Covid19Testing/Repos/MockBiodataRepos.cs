using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Repos
{
    public class MockBiodataRepos : IBiodataRepos
    {


        private List<TblBiodata> _biodata;

        GenderRepos genders = new GenderRepos();

        //string admin = "admin";

        public MockBiodataRepos()
        {
            //DateTime now = DateTime.Now;

            _biodata = new List<TblBiodata>()
            {
                   // new TblBiodata(){ Id = 1, Fullname = "Abdul Guillaume", Dateofbirth = DateTime.Parse("02/23/1982"), ResidentialAddress="Redroof", GenderNavigation = genders.GetById(1), InsertTime=now, InsertBy=admin, UpdateTime=now, UpdateBy=admin},
                   // new TblBiodata(){ Id = 2, Fullname = "Sterlande Louis", Dateofbirth = DateTime.Parse("04/04/1990"), ResidentialAddress="PaP", GenderNavigation = genders.GetById(2), InsertTime=now, InsertBy=admin, UpdateTime=now, UpdateBy=admin},
                   // new TblBiodata(){ Id = 2, Fullname = "Rolls Guillaume", Dateofbirth = DateTime.Parse("11/20/2021"), ResidentialAddress="PaP", GenderNavigation = genders.GetById(1), InsertTime=now, InsertBy=admin, UpdateTime=now, UpdateBy=admin}
            };
        }


        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TblBiodata obj)
        {
            //throw new NotImplementedException();
            obj.Id = _biodata.Max(e => e.Id) + 1;
            obj.GenderNavigation = genders.GetById(obj.Gender);
            //obj.UpdateBy = admin;
            //obj.UpdateTime = DateTime.Now;
            _biodata.Add(obj);
        }

        public void Delete(object id)
        {
            //throw new NotImplementedException();
            TblBiodata subject = GetById(id);

            if (subject != null)
            {
                _biodata.Remove(subject);
            }
        }

        public IEnumerable<TblBiodata> GetAll()
        {
            return _biodata;
        }

        public IEnumerable<TblBiodata> GetAllByName(string name)
        {
            //throw new NotImplementedException();
            return _biodata.Where(s => s.Fullname.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public TblBiodata GetByEPIDNo(object id)
        {
            //throw new NotImplementedException();
            return _biodata.FirstOrDefault(s => s.EpidNo == (string)id);

        }

        public TblBiodata GetById(object id)
        {
            //throw new NotImplementedException();
            return _biodata.FirstOrDefault(s => s.Id == (int)id);
        }


        public void Save(TblBiodata obj)
        {
            throw new NotImplementedException();
        }

        public void Update(TblBiodata obj)
        {
            TblBiodata subject = GetById(obj.Id);

            if (subject != null)
            {
                subject.Fullname = obj.Fullname;
                subject.LegalGardianName = obj.LegalGardianName;
                subject.Dateofbirth = obj.Dateofbirth;
                subject.Gender = obj.Gender;
                subject.GenderNavigation = genders.GetById(obj.Gender);
                //subject.GenderNavigation = obj.GenderNavigation;
                subject.EpidNo = obj.EpidNo;
                subject.HomePhone = obj.HomePhone;
                subject.ResidentialAddress = obj.ResidentialAddress;
                //subject.UpdateBy = admin;
                //subject.UpdateTime = DateTime.Now;
            }


            //throw new NotImplementedException();
        }
    }
}
