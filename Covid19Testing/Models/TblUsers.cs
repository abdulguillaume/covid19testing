using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblUsers
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Userrole { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime LastunlockTime { get; set; }
        public string LastunlockedBy { get; set; }
        public DateTime? LockTime { get; set; }
        public string LockedBy { get; set; }

        public TblRoles UserroleNavigation { get; set; }
        public TblUsersProfiles TblUsersProfiles { get; set; }
    }
}
