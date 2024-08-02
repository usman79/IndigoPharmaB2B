using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IndigoAdmin.DAL.Data.Repositories
{
    public class WishListRepository : RepositoryBase<WishList>, IWishListRepository
    {
        public WishListRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IWishListRepository : IRepository<WishList>
    {
    }
}
