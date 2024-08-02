using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class ProcessedOrder
    {
        public int OrderId { get; set; }
        public List<OrderDetail> returnDetails { get; set; } 
        public int PaymentType { get; set; }
        public float Payment { get; set; }
        public float Expense { get; set; }
        public ChequeDetail chequeDetail { get; set; }
    }
}
