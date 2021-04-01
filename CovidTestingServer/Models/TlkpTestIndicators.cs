using System;
using System.Collections.Generic;

namespace Covid19TestingServer.Models
{
    public partial class TlkpTestIndicators
    {
        public TlkpTestIndicators()
        {
            TblLabTestsIndicatorsValues = new HashSet<TblLabTestsIndicatorsValues>();
        }

        public int Id { get; set; }
        public int Method { get; set; }
        public string IndicatorName { get; set; }

        public TlkpTestMethods MethodNavigation { get; set; }
        public ICollection<TblLabTestsIndicatorsValues> TblLabTestsIndicatorsValues { get; set; }
    }
}
