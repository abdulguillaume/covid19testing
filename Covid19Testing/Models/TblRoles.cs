using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblRoles
    {
        public TblRoles()
        {
            TblUsers = new HashSet<TblUsers>();
        }

        public int Id { get; set; }
        public string Rolename { get; set; }

        public ICollection<TblUsers> TblUsers { get; set; }
    }
}
