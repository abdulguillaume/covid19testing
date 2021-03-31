using Covid19Testing.IRepos;
using Covid19Testing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Covid19Testing.Repos
{
    public class MailingRepos : IMailingRepos
    {
        public MailingRepos(Covid19TestingContext _Context)
        {
            Context = _Context;// new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(TblMailingLists obj)
        {
            //throw new NotImplementedException();
            Context.TblMailingLists.Add(obj);
            Save(obj);
        }

        public bool CreateGrp(TlkpMailingGroups grp)
        {
            //throw new NotImplementedException();
            //Context.TlkpMailingGroups.Add(grp);

            TlkpMailingGroups g = GetGrpByName(grp.GroupName);

            if (g != null) return false;

            Context.TlkpMailingGroups.Add(grp);
            Context.SaveChanges();

            return true;

        }

        public void Delete(object id)
        {
            TblMailingLists m = GetById(id);

            Context.TblEmailGroupMapping.RemoveRange(
                m.TblEmailGroupMapping.ToList()
            );

            Context.TblMailingLists.Remove(m);

            Context.SaveChanges();

        }

        public void DeleteGrp(TlkpMailingGroups grp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TblMailingLists> GetAll()
        {
            //throw new NotImplementedException();
            return Context.TblMailingLists
                 .Include(m => m.TblEmailGroupMapping);
                    
        }

        public TblMailingLists GetById(object id)
        {
            return Context.TblMailingLists
                .Include(m => m.TblEmailGroupMapping)
                .FirstOrDefault(m => m.Id == (int)id);
        }

        public void MapToGrp(TblMailingLists mail, TlkpMailingGroups grp)
        {
            TblMailingLists m = GetById(mail.Id);

            TblEmailGroupMapping _gp = m.TblEmailGroupMapping
               .FirstOrDefault(x => x.MailingGroup == grp.Id);

            if (_gp == null)
            {
                TblEmailGroupMapping mapping = new TblEmailGroupMapping();
                mapping.MailingGroupNavigation = grp;
                m.TblEmailGroupMapping.Add(mapping);
                Save(m);

            }
            
        }

        public void UnmapToGrp(TblMailingLists mail, TlkpMailingGroups grp)
        {
            //throw new NotImplementedException();
           
            TblMailingLists m = GetById(mail.Id);

            TblEmailGroupMapping _gp = m.TblEmailGroupMapping
               .FirstOrDefault(x => x.MailingGroup == grp.Id);

            Context.Remove(_gp);

            Save(m);

        }

        public void Save(TblMailingLists obj)
        {
            //throw new NotImplementedException();
            Context.SaveChanges();
            Context.Entry(obj).ReloadAsync();
        }    

        public void Update(TblMailingLists obj)
        {
            //throw new NotImplementedException();
            TblMailingLists mail = GetById(obj.Id);

            if (mail != null)
            {
                mail.Email = obj.Email;
                //mail.TblEmailGroupMapping.ad

                List<int> a = mail.TblEmailGroupMapping
                    .Select(x => x.MailingGroup.Value)
                    .ToList();

                List<int> b = obj.TblEmailGroupMapping
                    .Select(x => x.MailingGroup.Value)
                    .ToList();

                var a_except_b = a.Except(b).ToList(); //to remove
                var b_except_a = b.Except(a).ToList(); //to add

                List<TblEmailGroupMapping> toremove = mail.TblEmailGroupMapping
                    .Where(x => a_except_b.Contains(x.MailingGroup.Value))
                    .ToList();

                List<TblEmailGroupMapping> toadd = obj.TblEmailGroupMapping
                    .Where(x => b_except_a.Contains(x.MailingGroup.Value))
                    .ToList();

                Context.TblEmailGroupMapping.RemoveRange(toremove);
                Context.TblEmailGroupMapping.AddRange(toadd);

                Save(mail);
            }
        }

        public IEnumerable<TlkpMailingGroups> GetAllGrps()
        {
            //throw new NotImplementedException();
            return Context.TlkpMailingGroups;
        }

        public TlkpMailingGroups GetGrpByName(string grpname)
        {
            return Context.TlkpMailingGroups.FirstOrDefault(g => g.GroupName.ToLower() == grpname.ToLower());
        }

        public Dictionary<int, string> GetAllGrpsDict()
        {
            Dictionary<int, string> groups = new Dictionary<int, string>();

            foreach (var g in GetAllGrps())
            {
                groups[g.Id] = g.GroupName;
            }

            return groups;
        }
    }
}
