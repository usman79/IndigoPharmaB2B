using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndigoAdmin.DAL.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        IndigoDbContext Init();
    }
}
