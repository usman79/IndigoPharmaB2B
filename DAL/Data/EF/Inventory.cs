using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class Inventory
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InventoryId { get; set; }
        public int? ProductId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public float? PurchasePrice { get; set; }
        public int? Discount { get; set; }

        public string BatchNumber { get; set; }

        [NotMapped]
        public string ProductTitle { get; set; }
        [NotMapped]
        public string SupplierName { get; set; }
    }
}
