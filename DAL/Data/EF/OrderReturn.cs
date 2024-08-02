using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.DAL.Data.EF
{
    public class OrderReturn
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderReturnId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? OrderTakerId { get; set; }
        public int? DeliveryBoyId { get; set; }

        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public float Amount { get; set; }
        [NotMapped]
        public float Price { get; set; }
    }
}
