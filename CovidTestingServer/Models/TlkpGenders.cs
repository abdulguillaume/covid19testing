using System;
using System.Collections.Generic;

namespace Covid19TestingServer.Models
{
    public partial class TlkpGenders
    {
        public TlkpGenders()
        {
            TblBiodata = new HashSet<TblBiodata>();
        }

        public int Id { get; set; }
        public string Gender { get; set; }

        public ICollection<TblBiodata> TblBiodata { get; set; }
    }
}
