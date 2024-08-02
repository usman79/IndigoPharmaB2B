using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class UserToken
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long TokenId { get; set; }
        public Nullable<long> UserId { get; set; }
        public string AuthToken { get; set; }
        public Nullable<System.DateTime> IssueOn { get; set; }
        public Nullable<System.DateTime> ExpireOn { get; set; }
    }
}
