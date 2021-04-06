using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Covid19TestingServer.Models
{
    public partial class TblLabTestsIndicatorsValues
    {
        public int Id { get; set; }
        public int Labtest { get; set; }
        public int Indicator { get; set; }
        public int Method { get; set; }


        public decimal? IndicatorValue { get; set; }
        [JsonIgnore]
        public TblLabTests LabtestNavigation { get; set; }
        [JsonIgnore]
        public TlkpTestIndicators TlkpTestIndicators { get; set; }
    }
}
