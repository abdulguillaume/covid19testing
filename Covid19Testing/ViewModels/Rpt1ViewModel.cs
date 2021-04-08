using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Covid19Testing.Models;

namespace Covid19Testing.ViewModels
{
    public class Rpt1ViewModel
    {
        public Rpt1ViewModel()
        {
            grandTotal = new AggregationSampleTestedStatus();
            totalPerInterpretation = new List<AggregationSampleTestedInterpretation>();
            totalPerGenderNInterpretation = new List<AggregationGenderBreakdownInterpretation>();
        }

        public AggregationSampleTestedStatus grandTotal;
        public List<AggregationSampleTestedInterpretation> totalPerInterpretation;
        public List<AggregationGenderBreakdownInterpretation> totalPerGenderNInterpretation;
    }
}
