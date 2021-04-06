using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        [JsonIgnore]
        public ICollection<TblBiodata> TblBiodata { get; set; }
    }
}
