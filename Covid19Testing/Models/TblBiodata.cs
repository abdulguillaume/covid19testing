using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid19Testing.Models
{
    public partial class TblBiodata
    {
        public TblBiodata()
        {
            TblLabTests = new HashSet<TblLabTests>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        public string Fullname { get; set; }
        [DisplayName("Gardian")]
        public string LegalGardianName { get; set; }
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DisplayName("DOB")]
        public DateTime Dateofbirth { get; set; }
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        public int Gender { get; set; }
        [DisplayName("EPID-NO")]
        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        public string EpidNo { get; set; }
        //[Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [RegularExpression("^(\\+)[0-9]*", ErrorMessage = "Intl. format")]
        [DisplayName("Intl. Phone")]
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*", ErrorMessage = "Local format")]
        [DisplayName("Local Phone")]
        public string LocalPhone { get; set; }

        [Required(ErrorMessage = "*", AllowEmptyStrings = false)]
        [DisplayName("Address")]
        public string ResidentialAddress { get; set; }
        [DisplayName("Inserted on")]
        public DateTime? InsertTime { get; set; }
        [DisplayName("Inserted by")]
        public string InsertBy { get; set; }
        [DisplayName("Updated on")]
        public DateTime? UpdateTime { get; set; }
        [DisplayName("Updated by")]
        public string UpdateBy { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public TlkpGenders GenderNavigation { get; set; }
        public ICollection<TblLabTests> TblLabTests { get; set; }
    }
}
