using Covid19Testing.IRepos;
using Covid19Testing.Models;
using Covid19Testing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Covid19Testing.Repos
{
    public class LabTestRepos : ILabTestRepos
    {
        public LabTestRepos(Covid19TestingContext _Context)
        {
            Context = _Context;// new Covid19TestingContext();
        }

        public Covid19TestingContext Context { get; }

        public void Archive(object id)
        {
            throw new NotImplementedException();
        }

        public void Create(LabTestDetailsViewModel obj)
        {
            //throw new NotImplementedException();
            //LabTestDetailsViewModel tmp = new LabTestDetailsViewModel(obj); //not really needed

            TblLabTests test = obj.LabTest;

            if (test.Method == 0)
                throw new Exception("Select Method.");

            test.Approved = false;

            Context.TblLabTests.Add(test);

            obj.LabTest = test;

            Save(obj);
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LabTestDetailsViewModel> GetAll()
        {
            //throw new NotImplementedException();
            List<TblLabTests> _labTests = Context.TblLabTests
                .OrderByDescending(t => t.TestingDate)
                .Include(t=>t.BiodataNavigation)
                .ToList();

            if (_labTests == null) return null;

            List<LabTestDetailsViewModel> _labTestsVM = new List<LabTestDetailsViewModel>();

            foreach (var t in _labTests)
            {
                _labTestsVM.Add(new LabTestDetailsViewModel(t));
            }

            return _labTestsVM.AsEnumerable();


        }

        public IEnumerable<LabTestDetailsViewModel> GetAllByAfterDate(DateTime dt)
        {
            //throw new NotImplementedException();
            List<TblLabTests> _labTests = Context.TblLabTests
                .Where(t => t.TestingDate.Value.Date >= dt.Date )
                .Include(t => t.BiodataNavigation)
                .ToList();

            if (_labTests == null) return null;

            List<LabTestDetailsViewModel> _labTestsVM = new List<LabTestDetailsViewModel>();

            foreach (var t in _labTests)
            {
                _labTestsVM.Add(new LabTestDetailsViewModel(t));
            }

            return _labTestsVM.AsEnumerable();
        }

        public IEnumerable<LabTestDetailsViewModel> GetAllByBeforeDate(DateTime dt)
        {
            //throw new NotImplementedException();
            List<TblLabTests> _labTests = Context.TblLabTests
                .Where(t => t.TestingDate.Value.Date <= dt.Date)
                .Include(t => t.BiodataNavigation)
                .ToList();

            if (_labTests == null) return null;

            List<LabTestDetailsViewModel> _labTestsVM = new List<LabTestDetailsViewModel>();

            foreach (var t in _labTests)
            {
                _labTestsVM.Add(new LabTestDetailsViewModel(t));
            }

            return _labTestsVM.AsEnumerable();
        }

        public IEnumerable<LabTestDetailsViewModel> GetAllByDateRange(DateTime date1, DateTime date2)
        {
            //throw new NotImplementedException();
            List<TblLabTests> _labTests = Context.TblLabTests
                .Where(t => t.TestingDate.Value.Date >= date1.Date && t.TestingDate.Value.Date <= date2.Date)
                .Include(t => t.BiodataNavigation)
                .ToList();

            if (_labTests == null) return null;

            List<LabTestDetailsViewModel> _labTestsVM = new List<LabTestDetailsViewModel>();

            foreach (var t in _labTests)
            {
                _labTestsVM.Add(new LabTestDetailsViewModel(t));
            }

            return _labTestsVM.AsEnumerable();


        }

        public LabTestDetailsViewModel GetById(object id)
        {
            //throw new NotImplementedException();
            TblLabTests test = Context.TblLabTests
                .Include(t => t.MethodNavigation)
                .Include(t => t.BiodataNavigation)
                .Include(t => t.BiodataNavigation.GenderNavigation)
                .Include(t => t.TblLabTestsIndicatorsValues)
                .Include(t => t.TblLabTestsSpecimen)
                .FirstOrDefault(t => t.Id == (int)id);

            if (test != null)
                return new LabTestDetailsViewModel(test);

            throw new NotImplementedException();
        }

        public void Save(LabTestDetailsViewModel obj)
        {
            //throw new NotImplementedException();
            Context.SaveChanges();

            Context.Entry(obj.LabTest).Reload();
        }

        public void Update(LabTestDetailsViewModel obj)
        {
            //throw new NotImplementedException();

            //LabTestDetailsViewModel test = new LabTestDetailsViewModel(obj);//GetById(obj.LabTest.Id);

            TblLabTests test = Context.TblLabTests
                .Include(t=>t.TblLabTestsIndicatorsValues)
                .Include(t=>t.TblLabTestsSpecimen)
                .FirstOrDefault(t=>t.Id==obj.LabTest.Id);

            if (test != null)
            {
                test.Interpretation = obj.LabTest.Interpretation;
                test.TestingDate = obj.LabTest.TestingDate;
                test.TestingTime = obj.LabTest.TestingTime;
                test.ReportingDate = obj.LabTest.ReportingDate;
                test.ReportingTime = obj.LabTest.ReportingTime;
                test.UpdateBy = obj.LabTest.UpdateBy;
                test.Approved = obj.LabTest.Approved;
                if (test.Approved == null) test.Approved = false;

                //test.Update(obj); //do i really need it.
                for (int k = 0; k < test.TblLabTestsIndicatorsValues.Count; k++)
                {
                    TblLabTestsIndicatorsValues i = test.TblLabTestsIndicatorsValues.ElementAt(k);
                    i.IndicatorName = obj.Indicators.ElementAt(k).IndicatorName;
                    i.IndicatorValue = obj.Indicators.ElementAt(k).IndicatorValue;
                }

                for (int k = 0; k < test.TblLabTestsSpecimen.Count; k++)
                {
                    TblLabTestsSpecimen i = test.TblLabTestsSpecimen.ElementAt(k);
                    i.SpecimenName = obj.Specimen.ElementAt(k).SpecimenName;
                    i.Checked = obj.Specimen.ElementAt(k).Checked;
                    i.SpecimenOther = obj.Specimen.ElementAt(k).SpecimenOther;
                }

                obj.LabTest = test;

                Save(obj);
            }
            else if(test!=null && test.Method!=obj.LabTest.Method){
                throw new Exception("Method already set. Cannot be changed.");
            }
        }
    }
}
