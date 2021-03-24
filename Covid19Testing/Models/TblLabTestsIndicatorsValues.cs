using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblLabTestsIndicatorsValues
    {
        public int Id { get; set; }
        public int Labtest { get; set; }
        public int Indicator { get; set; }
        public int Method { get; set; }
        public decimal? IndicatorValue { get; set; }
        public DateTime? InsertTime { get; set; }
        public string InsertBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateBy { get; set; }

        public TblLabTests LabtestNavigation { get; set; }
        public TlkpTestIndicators TlkpTestIndicators { get; set; }
    }
}
