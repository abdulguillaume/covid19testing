using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Covid19TestingServer.Models
{
    public partial class TlkpSpecimen
    {
        public TlkpSpecimen()
        {
            TblLabTestsSpecimen = new HashSet<TblLabTestsSpecimen>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public ICollection<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
    }
}
