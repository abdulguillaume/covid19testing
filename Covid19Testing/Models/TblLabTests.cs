using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        public int Method { get; set; }
        public int Interpretation { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Test date")]
        public DateTime? TestingDate { get; set; }
        [Required(ErrorMessage = "*")]
        [DisplayName("Test time")]
        public TimeSpan? TestingTime { get; set; }
        [DisplayName("Report date")]
        public DateTime? ReportingDate { get; set; }
        [DisplayName("Report time")]
        public TimeSpan? ReportingTime { get; set; }
        [DisplayName("Inserted on")]
        public DateTime? InsertTime { get; set; }
        [DisplayName("Inserted by")]
        public string InsertBy { get; set; }
        [DisplayName("Updated on")]
        public DateTime? UpdateTime { get; set; }
        [DisplayName("Updated by")]
        public string UpdateBy { get; set; }
        public bool? Approved { get; set; }

        public TblBiodata BiodataNavigation { get; set; }
        public TlkpTestMethods MethodNavigation { get; set; }
        public ICollection<TblLabTestsIndicatorsValues> TblLabTestsIndicatorsValues { get; set; }
        public ICollection<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
    }
}
