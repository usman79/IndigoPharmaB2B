using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class OrderDetailList
    {
        public int OrderId { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
