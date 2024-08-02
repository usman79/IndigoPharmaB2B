using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IndigoAdmin.DAL.Data.Repositories
{
    public class OrderReturnRepository : RepositoryBase<OrderReturn>, IOrderReturnRepository
{
    public OrderReturnRepository(IDbFactory dbFactory) : base(dbFactory) { }
}

public interface IOrderReturnRepository : IRepository<OrderReturn>
{
}
}
