using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.EntityFrameworkCore;

namespace Covid19Testing.Repos
{
    public class BiodataRepos : IBiodataRepos
    {
        //GenderRepos genders = new GenderRepos();

        //string admin = "admin";

        public BiodataRepos(Covid19TestingContext _Context)
        {
            Context = _Context;// new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TblBiodata obj)
        {
            //throw new NotImplementedException();
            
            //obj.InsertBy=
            //obj.UpdateBy = admin;

            Context.TblBiodata.Add(obj);

            //obj.InsertTime =
            //obj.UpdateTime = DateTime.Now;

            Save(obj);
        }

        public void Delete(int  id, string username)
        {
            //throw new NotImplementedException();
            TblBiodata s = GetById(id);

            if (s == null)
                return;

            var cmd = Context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "[dbo].[sp_delete_biodata] @biodata, @username";

            var param1 = cmd.CreateParameter();
            param1.ParameterName = "@biodata";
            param1.Value = id;
            cmd.Parameters.Add(param1);

            var param2 = cmd.CreateParameter();
            param2.ParameterName = "@username";
            param2.Value = username;
            cmd.Parameters.Add(param2);

            try
            {

                Context.Database.GetDbConnection().Open();
                // Run the sproc
                var reader = cmd.ExecuteReader();

            }
            catch
            {

            }
            finally
            {
                Context.Database.GetDbConnection().Close();
            }
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
            
        }

        public IEnumerable<TblBiodata> GetAll()
        {
            return Context.TblBiodata
                .Include(g=>g.GenderNavigation);
        }

        public IEnumerable<TblBiodata> GetAllByName(string name)
        {
            //throw new NotImplementedException();
            return Context.TblBiodata
                .Include(g=>g.GenderNavigation)
                .Where(x => x.Fullname.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public TblBiodata GetByEPIDNo(object id)
        {
            return Context.TblBiodata
                .Include(b => b.GenderNavigation)
                .FirstOrDefault(x=>((string)id).CompareTo(x.EpidNo)==0);
        }

        public TblBiodata GetById(object id)
        {
            return Context.TblBiodata.Include(b=>b.GenderNavigation)
                .FirstOrDefault(b=>b.Id==(int)id);
        }

        public void Save(TblBiodata obj)
        {
            //throw new NotImplementedException();
            Context.SaveChanges();
            Context.Entry(obj).ReloadAsync();

        }

        public int TestsCount(int biodata)
        {
            //throw new NotImplementedException();
            return Context.TblLabTests.Count(s => s.Biodata == biodata);
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
                subject.Email = obj.Email;
                subject.EpidNo = obj.EpidNo;
                subject.LocalPhone = obj.LocalPhone;
                subject.HomePhone = obj.HomePhone;
                subject.ResidentialAddress = obj.ResidentialAddress;
                subject.UpdateBy = obj.UpdateBy;

                Save(subject);
            }
        }
    }
}
