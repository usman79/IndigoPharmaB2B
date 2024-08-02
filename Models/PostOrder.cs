using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class PostOrder
    {
        public string Customer { get; set; }

        public int CustomerId { get; set; }
        public string Store { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public int Coins { get; set; }
        public int PaymentMethod { get; set; }

        public List<PostProductOrder> Cart { get; set; }
    }
}
