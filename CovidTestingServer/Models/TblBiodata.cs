using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Covid19TestingServer.Models
{
    public partial class TblBiodata
    {
        public TblBiodata()
        {
            TblLabTests = new HashSet<TblLabTests>();
        }

        public int Id { get; set; }

        public string Fullname { get; set; }

        [DisplayName("Gardian")]
        public string LegalGardianName { get; set; }

        [DisplayName("DOB")]
        public DateTime Dateofbirth { get; set; }


        public int Gender { get; set; }
        [DisplayName("EPID-NO")]
        public string EpidNo { get; set; }

        [DisplayName("Intl. Phone")]
        public string HomePhone { get; set; }

        [DisplayName("Local Phone")]
        public string LocalPhone { get; set; }

        [DisplayName("Address")]
        public string ResidentialAddress { get; set; }
        
        [DisplayName("Transfered on")]
        public DateTime TransferTime { get; set; }

        public string Email { get; set; }

        public TlkpGenders GenderNavigation { get; set; }
        public ICollection<TblLabTests> TblLabTests { get; set; }
    }
}
