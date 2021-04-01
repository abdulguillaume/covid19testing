using System;
using System.Collections.Generic;

namespace Covid19TestingServer.Models
{
    public partial class TblLabTestsSpecimen
    {
        public int Id { get; set; }
        public int Labtest { get; set; }
        public int Specimen { get; set; }
        public string SpecimenOther { get; set; }
        public bool Checked { get; set; }

        public TblLabTests LabtestNavigation { get; set; }
        public TlkpSpecimen SpecimenNavigation { get; set; }
    }
}
