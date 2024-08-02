using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.DAL.Data.Repositories
{
    class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }

    public interface IProductRepository : IRepository<Product>
    {
    }
}
