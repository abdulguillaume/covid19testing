using Covid19Testing.IRepos;
using Covid19Testing.Models;
using Covid19Testing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Repos
{
    public class LabTestRepos : ILabTestRepos
    {
        public LabTestRepos()
        {
            Context = new Covid19TestingContext();
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

            Context.TblLabTests.Add(test);

            Save();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LabTestDetailsViewModel> GetAll()
        {
            //throw new NotImplementedException();
            List<TblLabTests> _labTests = Context.TblLabTests.ToList();

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
            TblLabTests test = Context.TblLabTests.Find(id);
            if (test != null)
                return new LabTestDetailsViewModel(test);

            throw new NotImplementedException();
        }

        public void Save()
        {
            //throw new NotImplementedException();
            Context.SaveChanges();
        }

        public void Update(LabTestDetailsViewModel obj)
        {
            //throw new NotImplementedException();

            //LabTestDetailsViewModel test = new LabTestDetailsViewModel(obj);//GetById(obj.LabTest.Id);

            TblLabTests test = Context.TblLabTests.Find(obj.LabTest.Id);

            if (test != null)
            {
                test.Interpretation = obj.LabTest.Interpretation;
                test.TestingDate = obj.LabTest.TestingDate;
                test.TestingTime = obj.LabTest.TestingTime;
                test.ReportingDate = obj.LabTest.ReportingDate;
                test.ReportingTime = obj.LabTest.ReportingTime;

                //test.Update(obj); //do i really need it.
                for (int k = 0; k < test.TblLabTestsIndicatorsValues.Count; k++)
                {
                    TblLabTestsIndicatorsValues i = test.TblLabTestsIndicatorsValues.ElementAt(k);
                    i.IndicatorValue = obj.Indicators.ElementAt(k).IndicatorValue;
                }

                for (int k = 0; k < test.TblLabTestsSpecimen.Count; k++)
                {
                    TblLabTestsSpecimen i = test.TblLabTestsSpecimen.ElementAt(k);
                    i.Checked = obj.Specimen.ElementAt(k).Checked;
                    i.SpecimenOther = obj.Specimen.ElementAt(k).SpecimenOther;
                }

                Save();
            }
            else if(test!=null && test.Method!=obj.LabTest.Method){
                throw new Exception("Method already set. Cannot be changed.");
            }
        }
    }
}
