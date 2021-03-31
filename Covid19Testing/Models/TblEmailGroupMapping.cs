using System;
using System.Collections.Generic;

namespace Covid19Testing.Models
{
    public partial class TblEmailGroupMapping
    {
        public int Id { get; set; }
        public int Email { get; set; }
        public int? MailingGroup { get; set; }

        public TblMailingLists EmailNavigation { get; set; }
        public TlkpMailingGroups MailingGroupNavigation { get; set; }
    }
}
