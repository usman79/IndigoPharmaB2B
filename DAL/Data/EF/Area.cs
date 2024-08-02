using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class Area
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    
       
    }
}
