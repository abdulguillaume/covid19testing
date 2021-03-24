using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using Covid19Testing.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid19Testing.Models
{
    ////[ModelMetadataType(typeof(TblBiodata_MD))]
    //public partial class TblBiodata
    //{

    //}

    ////[ModelMetadataType(typeof(TblLabTests_MD))]
    //public partial class TblLabTests
    //{

    //}

    public partial class TblLabTestsIndicatorsValues
    {
        [NotMapped]
        public string IndicatorName { get; set; }
    }

    public partial class TblLabTestsSpecimen
    {
        [NotMapped]
        public string SpecimenName { get; set; }
    }
}
