
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class SalesmanReportModel
    {
        public string Name { get; set; }
        public int OrderId { get; set; }
        public float SalesAmount { get; set; }
        public float PurchaseAmount { get; set; }
        public float ReturnSalesAmount { get; set; }
        public float ReturnPurchaseAmount { get; set; }
    }
}
