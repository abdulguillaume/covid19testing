using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19Testing.IRepos;
using Covid19Testing.Models;

namespace Covid19Testing.ViewModels
{
    public class LabTestDetailsViewModel
    {
        private readonly IMethodRepos methods;
        //private readonly ITestIndicatorRepos indicators;
        /*public LabTestDetailsViewModel(LabTestDetailsViewModel obj)
        {
            BioData = obj.BioData;
            LabTest = obj.LabTest;
            Method = obj.Method;
            Indicators = obj.Indicators;
            Specimen = obj.Specimen;
        }*/

        public LabTestDetailsViewModel() { }

        public LabTestDetailsViewModel(TblLabTests test)
        {//perfect
            BioData = test.BiodataNavigation;
            LabTest = test;
            //Method = test.MethodNavigation;
            Indicators = test.TblLabTestsIndicatorsValues.ToList();
            Specimen = test.TblLabTestsSpecimen.ToList();
        }

        public LabTestDetailsViewModel(TblBiodata _BioData, IMethodRepos _methods, ISpecimenRepos _specimen) //main to create
        {//perfect

            BioData = _BioData;
            //Method = _method;
            methods = _methods;

            //Indicators = new List<TblLabTestsIndicatorsValues>();
            Specimen = new List<TblLabTestsSpecimen>();

            LabTest = new TblLabTests();
            LabTest.Biodata = _BioData.Id;
            //LabTest.Method = _method.Id;


            /*foreach(var i in _method.TlkpTestIndicators)
            {
                TblLabTestsIndicatorsValues v = new TblLabTestsIndicatorsValues();
                v.Indicator = i.Id;
                v.Method = _method.Id;
                LabTest.TblLabTestsIndicatorsValues.Add(v);

                Indicators.Add(v);


            }*/


            foreach (var t in _specimen.GetAll())
            {
                TblLabTestsSpecimen s = new TblLabTestsSpecimen();
                s.Specimen = t.Id;
                s.SpecimenName = t.Type;
                LabTest.TblLabTestsSpecimen.Add(s);
                Specimen.Add(s);
            }


        }

        /*public void SetMethod(int Id)
        {
            
            Indicators = new List<TblLabTestsIndicatorsValues>();

            LabTest.Method = Id;

            foreach(var i in methods.GetById(Id).TlkpTestIndicators)
            {
                TblLabTestsIndicatorsValues v = new TblLabTestsIndicatorsValues();
                v.Indicator = i.Id;
                v.Method = Id;
                LabTest.TblLabTestsIndicatorsValues.Add(v);

                Indicators.Add(v);


            }

        }*/

        /*public void Update(LabTestDetailsViewModel obj)
        {
            if (obj.LabTest.Id == LabTest.Id)
            {
               
            }
        }*/


        public TblLabTests LabTest { get; set; }
        public TblBiodata BioData { get; set; }
        //public TlkpTestMethods Method { get; set; }
        public List<TblLabTestsIndicatorsValues> Indicators { get; set; }
        public List<TblLabTestsSpecimen> Specimen { get; set; }
    }
}
