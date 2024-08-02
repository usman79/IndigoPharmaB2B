using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class UserRolePermission
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long UserRolePermissionId { get; set; }
        public int? UserRoleId { get; set; }
        public int? PermissionId { get; set; }
    }
}
