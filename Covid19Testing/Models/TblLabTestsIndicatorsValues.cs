using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Covid19Testing.Models
{
    public partial class TblLabTestsIndicatorsValues
    {
        public int Id { get; set; }
        public int Labtest { get; set; }
        public int Indicator { get; set; }
        public int Method { get; set; }
        [Required(ErrorMessage = "*")]
        public decimal? IndicatorValue { get; set; }
        public DateTime? InsertTime { get; set; }
        public string InsertBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateBy { get; set; }
        [JsonIgnore]
        public TblLabTests LabtestNavigation { get; set; }
        public TlkpTestIndicators TlkpTestIndicators { get; set; }
    }
}
