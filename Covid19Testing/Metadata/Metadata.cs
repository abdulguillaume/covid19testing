using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Testing.Metadata
{
    public class TblBiodata_MD
    {
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        public string Fullname;

        [DisplayName("Gardian")]
        public string LegalGardianName { get; set; }

        //[Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DisplayName("DOB")]
        [DataType(DataType.Date)]
        public DateTime Dateofbirth;

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        public int Gender;

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [RegularExpression("^(\\+)[0-9]*", ErrorMessage = "*")]
        public string HomePhone;

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DisplayName("Address")]
        public string ResidentialAddress;

    }

    public class TblLabTests_MD
    {
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        public DateTime? TestingDate;
        
        [DataType(DataType.Date)]
        public DateTime? ReportingDate;

    }
}
