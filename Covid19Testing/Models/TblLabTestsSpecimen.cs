using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblLabTestsSpecimen
    {
        public int Id { get; set; }
        public int Labtest { get; set; }
        public int Specimen { get; set; }
        public string SpecimenOther { get; set; }
        public DateTime? InsertTime { get; set; }
        public string InsertBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        public bool Checked { get; set; }

        public TblLabTests LabtestNavigation { get; set; }
        public TlkpSpecimen SpecimenNavigation { get; set; }
    }
}
