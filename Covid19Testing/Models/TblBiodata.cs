using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblBiodata
    {
        public TblBiodata()
        {
            TblLabTests = new HashSet<TblLabTests>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string LegalGardianName { get; set; }
        public DateTime Dateofbirth { get; set; }
        public int Gender { get; set; }
        public string EpidNo { get; set; }
        public string HomePhone { get; set; }
        public string ResidentialAddress { get; set; }
        public DateTime InsertTime { get; set; }
        public string InsertBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }

        public TlkpGenders GenderNavigation { get; set; }
        public ICollection<TblLabTests> TblLabTests { get; set; }
    }
}
