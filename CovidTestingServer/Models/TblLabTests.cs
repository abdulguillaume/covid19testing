
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid19TestingServer.Models
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
        [DisplayName("Test date")]
        public DateTime? TestingDate { get; set; }
        [DisplayName("Test time")]
        public TimeSpan? TestingTime { get; set; }
        [DisplayName("Report date")]
        public DateTime? ReportingDate { get; set; }
        [DisplayName("Report time")]
        public TimeSpan? ReportingTime { get; set; }
        
        [DisplayName("Published on")]
        public DateTime? TransferTime { get; set; }

        public TblBiodata BiodataNavigation { get; set; }
        public TlkpTestMethods MethodNavigation { get; set; }
        public ICollection<TblLabTestsIndicatorsValues> TblLabTestsIndicatorsValues { get; set; }
        public ICollection<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
    }
}
