using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository modelRepository;
        private readonly IBrandRepository brandRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IProductRepository modelRepository, IBrandRepository brandRepository, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.brandRepository = brandRepository;
            this.inventoryRepository = inventoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public Product GetById(long id)
        {
            var obj = modelRepository.Get(x => x.ProductId == id);

            return obj;
        }

        public IEnumerable<Product> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }
        
        public IEnumerable<Product> GetAllByBrand(int BrandId)
        {
            var objs = modelRepository.GetMany(x=>x.BrandId==BrandId);
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public int GetCount()
        {
            var objs = modelRepository.Get().Count();
            return objs;
        }
        public IEnumerable<Product> GetAllFiltered(string text, int page)
        {
            var query = from product in modelRepository.Get()
                        join brand in brandRepository.Get() on product.BrandId equals brand.BrandId
                        where (product.Title.Contains(text) || brand.Title.Contains(text))
                        select new Product() { 
                        ProductId=product.ProductId,
                        BarCode=product.BarCode,
                        BatchNumber=product.BatchNumber,
                        BrandId=product.BrandId,
                        CategoryId=product.CategoryId,
                        CreatedAt=product.CreatedAt,
                        CreatedBy=product.CreatedBy,
                        Discount=product.Discount,
                        Image=product.Image,
                        MaxPerOrder=product.MaxPerOrder,
                        MedLogo=product.MedLogo,
                        MinWarningLimit=product.MinWarningLimit,
                        ModifiedAt=product.ModifiedAt,
                        ModifiedBy=product.ModifiedBy,
                        Price= product.Price??0,
                        Quantity=product.Quantity,
                        Summary=product.Summary,
                        Title=product.Title
                        };
            var objs = query.Skip(page * 100).Take(100);
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(Product objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.ProductId;
        }

        public void Update(Product objModel)
        {
            modelRepository.Update(objModel);
            Commit();
        }

        public void Delete(int id)
        {
            modelRepository.Delete(id);
            Commit();
        }

        public void Commit()
        {
            unitOfWork.Commit();
        }

    }
    public interface IProductService : IService
    {
        Product GetById(long id);
        IEnumerable<Product> GetAll();
    
        IEnumerable<Product> GetAllByBrand(int BrandId);
        int GetCount();
        IEnumerable<Product> GetAllFiltered(string text,int page);
        long Create(Product objModel);
        void Update(Product objModel);
        void Delete(int id);
    }
}
