using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace IndigoAdmin.DAL.Data.EF
{
    public partial class Product
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public string Image { get; set; }
        public string BarCode { get; set; }
        public string BatchNumber { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public int? Discount { get; set; }
        public int? TPDiscount { get; set; }
        public int? MaxPerOrder { get; set; }
        public int? MinWarningLimit { get; set; }

        [NotMapped]
        public IFormFile MedLogo { get; set; }
    }
}
