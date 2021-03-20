using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
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
        public DateTime InsertTime { get; set; }
        public string InsertBy { get; set; }

        public ICollection<TblLabTests> TblLabTests { get; set; }
        public ICollection<TlkpTestIndicators> TlkpTestIndicators { get; set; }
    }
}
