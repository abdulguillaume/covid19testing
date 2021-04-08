using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Models
{
    [NotMapped]
    public class AggregationSampleTestedStatus
    {
        public int SampleTested { get; set; }
        public int ProcessingInLab { get; set; }
        public int PendingApproval { get; set; }
        public int Approved { get; set; }
        public int Published { get; set; }
    }


    [NotMapped]
    public class AggregationSampleTestedInterpretation
    {
        public string Interpretation { get; set; }
        public int SampleTested { get; set; }
        public int ProcessingInLab { get; set; }
        public int PendingApproval { get; set; }
        public int Approved { get; set; }
        public int Published { get; set; }
    }

    [NotMapped]
    public class AggregationGenderBreakdownInterpretation
    {
        public string Interpretation { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Indeterminate { get; set; }
        public int NonConforming { get; set; }
        public int TransgenderMale { get; set; }
        public int TransgenderFemale { get; set; }
        public int Unknown { get; set; }
    }
}
