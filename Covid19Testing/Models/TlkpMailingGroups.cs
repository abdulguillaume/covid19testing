using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid19Testing.Models
{
    public partial class TlkpMailingGroups
    {
        public TlkpMailingGroups()
        {
            TblEmailGroupMapping = new HashSet<TblEmailGroupMapping>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        [DisplayName("Group")]
        public string GroupName { get; set; }

        public ICollection<TblEmailGroupMapping> TblEmailGroupMapping { get; set; }
    }
}
