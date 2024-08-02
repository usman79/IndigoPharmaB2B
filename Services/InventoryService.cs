 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository modelRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserAccountRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public InventoryService(IInventoryRepository modelRepository, IProductRepository productRepository, IUserAccountRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public Inventory GetById(long id)
        {
            var obj = modelRepository.Get(x => x.InventoryId == id);

            return obj;
        }

        public IEnumerable<Inventory> GetAll()
        {
            var query = from inventory in modelRepository.Get()
                        join user in userRepository.Get() on inventory.SupplierId equals (int)user.UserId into result
                        from user in result.DefaultIfEmpty()
                        join pro in productRepository.Get() on inventory.ProductId equals pro.ProductId into result2
                        from pro in result2.DefaultIfEmpty()
                        select new Inventory()
                        {
                            InventoryId = inventory.InventoryId,
                            CreatedAt = inventory.CreatedAt,
                            CreatedBy = inventory.CreatedBy,
                            Discount = inventory.Discount,
                            ModifiedAt = inventory.ModifiedAt,
                            ModifiedBy = inventory.ModifiedBy,
                            Price = inventory.Price,
                            ProductId = inventory.ProductId,
                            Quantity = inventory.Quantity,
                            SupplierId = inventory.SupplierId,
                            BatchNumber = inventory.BatchNumber,

                            ProductTitle =pro.Title,
                            SupplierName=user.UserFirstName +" "+user.UserLastName
                        };
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public IEnumerable<Inventory> GetAllBySupplier(int Id)
        {
            var query = from inventory in modelRepository.Get()
                        join user in userRepository.Get() on inventory.SupplierId equals (int)user.UserId into result
                        from user in result.DefaultIfEmpty()
                        join pro in productRepository.Get() on inventory.ProductId equals pro.ProductId into result2
                        from pro in result2.DefaultIfEmpty()
                        where inventory.SupplierId==Id
                        select new Inventory()
                        {
                            InventoryId = inventory.InventoryId,
                            CreatedAt = inventory.CreatedAt,
                            CreatedBy = inventory.CreatedBy,
                            Discount = inventory.Discount,
                            ModifiedAt = inventory.ModifiedAt,
                            ModifiedBy = inventory.ModifiedBy,
                            Price = inventory.Price,
                            ProductId = inventory.ProductId,
                            Quantity = inventory.Quantity,
                            SupplierId = inventory.SupplierId,
                            BatchNumber=inventory.BatchNumber,

                            ProductTitle = pro.Title,
                            SupplierName = user.UserFirstName + " " + user.UserLastName
                        };
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public long Create(Inventory objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.InventoryId;
        }

        public void Update(Inventory objModel)
        {
            modelRepository.Update(objModel);
            Commit();
        }

        public void Delete(long id)
        {
            modelRepository.Delete(id);
            Commit();
        }

        public void Commit()
        {
            unitOfWork.Commit();
        }

    }
    public interface IInventoryService : IService
    {
        Inventory GetById(long id);
        IEnumerable<Inventory> GetAll();
        IEnumerable<Inventory> GetAllBySupplier(int Id);
        long Create(Inventory objModel);
        void Update(Inventory objModel);
        void Delete(long id);
    }
}
