
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IndigoAdmin.DAL.Data.Repositories
{
    public class ChequeDetailRepository : RepositoryBase<ChequeDetail>, IChequeDetailRepository
    {
        public ChequeDetailRepository (IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IChequeDetailRepository  : IRepository<ChequeDetail >
    {
    }
}
