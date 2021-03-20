using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

using Covid19Testing.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Covid19Testing.Models
{
    [ModelMetadataType(typeof(TblBiodata_MD))]
    public partial class TblBiodata
    {

    }
}
