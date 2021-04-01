using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19TestingServer.Models;

namespace Covid19TestingServer.ViewModels
{
    public class LabTestDetailsViewModel
    {


        public LabTestDetailsViewModel() { }

        public LabTestDetailsViewModel(TblLabTests test)
        {//perfect
            BioData = test.BiodataNavigation;
            LabTest = test;
            //Method = test.MethodNavigation;
            Indicators = test.TblLabTestsIndicatorsValues.ToList();
            Specimen = test.TblLabTestsSpecimen.ToList();
        }

        public TblLabTests LabTest { get; set; }
        public TblBiodata BioData { get; set; }
        //public TlkpTestMethods Method { get; set; }
        public List<TblLabTestsIndicatorsValues> Indicators { get; set; }
        public List<TblLabTestsSpecimen> Specimen { get; set; }
    }
}
