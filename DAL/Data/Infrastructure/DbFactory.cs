using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.EF;

namespace IndigoAdmin.DAL.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        IndigoDbContext dbContext;

        public IndigoDbContext Init()
        {
            return dbContext ?? (dbContext = new IndigoDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
