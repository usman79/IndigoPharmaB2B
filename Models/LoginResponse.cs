using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Models
{
    public class LoginResponse
    {
        public int httpCode { get; set; }
        public UserAccount userData { get; set; }
    }
}
