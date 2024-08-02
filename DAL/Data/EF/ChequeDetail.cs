using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.DAL.Data.EF
{
    public class ChequeDetail
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ChequeDetailId { get; set; }
        public int OrderId { get; set; }
        public int Status { get; set; }//1 Pending, 2 Success, 3 Rejected
        public string ChequeNumber { get; set; }
        public string BankName { get; set; }
        public float? Amount { get; set; }
        public DateTime? Date { get; set; }
    }
}
