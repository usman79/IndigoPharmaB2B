using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class Order
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? OrderTakerId { get; set; }
        public int? DeliveryBoyId { get; set; }
        public short? OrderType { get; set; }
        public int? PaymentType { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public string BillingAddress { get; set; }
        public string BillingPhone { get; set; }
        public string Store { get; set; }
        public string Logitude { get; set; }
        public string Latitude { get; set; }
        public string UserName { get; set; }

        [NotMapped]
        public float TotalAmount { get; set; }
        [NotMapped]
        public float PaidAmount { get; set; }
    }
}
