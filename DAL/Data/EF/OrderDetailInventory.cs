using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IndigoAdmin.DAL.Data.EF
{
    public class OrderDetailInventory
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderDetailInventoryId { get; set; }
        public int OrderDetailsId { get; set; }
        public int InventoryId { get; set; }
        public int? Quantity { get; set; }
        
    }
}
 
     
   