using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid19Testing.Models
{
    public partial class TblMailingLists
    {
        public TblMailingLists()
        {
            TblEmailGroupMapping = new HashSet<TblEmailGroupMapping>();
        }

        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayName("Inserted on")]
        public DateTime? InsertTime { get; set; }
        [DisplayName("Inserted by")]
        public string InsertBy { get; set; }

        public ICollection<TblEmailGroupMapping> TblEmailGroupMapping { get; set; }
    }
}
