using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblUsersProfiles
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public TblUsers UsernameNavigation { get; set; }
    }
}
