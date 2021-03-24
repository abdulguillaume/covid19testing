using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Covid19Testing.Models
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
