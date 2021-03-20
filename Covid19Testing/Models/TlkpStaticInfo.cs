using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TlkpStaticInfo
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public string Unit { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string LabSupervisor { get; set; }
        public byte[] LabSupervisorSignature { get; set; }
        public string Miscellaneous { get; set; }
        public DateTime InsertTime { get; set; }
        public string InsertBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateBy { get; set; }
    }
}
