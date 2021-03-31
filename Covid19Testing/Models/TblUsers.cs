using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid19Testing.Models
{
    public partial class TblUsers
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [DisplayName("Role")]
        public int Userrole { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? InsertTime { get; set; }
        [DisplayName("Last unlocked on")]
        public DateTime? LastunlockTime { get; set; }
        [DisplayName("Last unlocked by")]
        public string LastunlockedBy { get; set; }
        [DisplayName("Locked on")]
        public DateTime? LockTime { get; set; }
        [DisplayName("Locked by")]
        public string LockedBy { get; set; }
        [DisplayName("Updated on")]
        public DateTime? UpdateTime { get; set; }
        [DisplayName("Updated by")]
        public string UpdateBy { get; set; }

        public TblRoles UserroleNavigation { get; set; }
        public TblUsersProfiles TblUsersProfiles { get; set; }
    }
}
