using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Covid19TestingServer.Models
{
    public partial class TlkpTestMethods
    {
        public TlkpTestMethods()
        {
            TblLabTests = new HashSet<TblLabTests>();
            TlkpTestIndicators = new HashSet<TlkpTestIndicators>();
        }

        public int Id { get; set; }
        public string Methodname { get; set; }

        [JsonIgnore]
        public ICollection<TblLabTests> TblLabTests { get; set; }
        [JsonIgnore]
        public ICollection<TlkpTestIndicators> TlkpTestIndicators { get; set; }
    }
}
