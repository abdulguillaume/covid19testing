using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TlkpSpecimen
    {
        public TlkpSpecimen()
        {
            TblLabTestsSpecimen = new HashSet<TblLabTestsSpecimen>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
    }
}
