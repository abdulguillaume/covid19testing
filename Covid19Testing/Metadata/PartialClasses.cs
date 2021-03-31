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

    public partial class TblLabTests
    {
        [NotMapped]
        public bool _Interpretation { get { return Interpretation!=97; } set {; } }
    }

        public partial class TblBiodata
    {
        [NotMapped]
        public string _genderName { get; set; }

        [NotMapped]
        public string _dob { get; set; }  //
    }

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

    public partial class TblUsers
    {
        [NotMapped]
        public bool locked { get { return LockTime != null; } set {; } }
    }

    public partial class TblEmailGroupMapping {
        [NotMapped]
        public bool add { get; set; } = false;
    }
}
