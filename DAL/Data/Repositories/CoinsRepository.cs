using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.DAL.Data.Infrastructure;

namespace IndigoAdmin.DAL.Data.Repositories
{
    public class CoinsRepository:RepositoryBase<Coins>, ICoinsRepository
    {
        public CoinsRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
 

    public interface ICoinsRepository : IRepository<Coins>
    {
    }