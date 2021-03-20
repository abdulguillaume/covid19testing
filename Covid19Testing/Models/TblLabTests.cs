using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblLabTests
    {
        public TblLabTests()
        {
            TblLabTestsIndicatorsValues = new HashSet<TblLabTestsIndicatorsValues>();
            TblLabTestsSpecimen = new HashSet<TblLabTestsSpecimen>();
        }

        public int Id { get; set; }
        public int Biodata { get; set; }
        public int Method { get; set; }
        public int Interpretation { get; set; }
        public DateTime? TestingDate { get; set; }
        public TimeSpan? TestingTime { get; set; }
        public DateTime? ReportingDate { get; set; }
        public TimeSpan? ReportingTime { get; set; }
        public DateTime InsertTime { get; set; }
        public string InsertBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }

        public TblBiodata BiodataNavigation { get; set; }
        public TlkpTestMethods MethodNavigation { get; set; }
        public ICollection<TblLabTestsIndicatorsValues> TblLabTestsIndicatorsValues { get; set; }
        public ICollection<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
    }
}
