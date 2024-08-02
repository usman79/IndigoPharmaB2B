using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class PostProductOrder
    {
        public ProductModal Product { get; set; }
        public int? Quantity { get; set; }
    }
}
