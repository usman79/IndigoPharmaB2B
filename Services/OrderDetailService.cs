 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository modelRepository;
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderDetailService(IOrderDetailRepository modelRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public OrderDetail GetById(long id)
        {
            var obj = modelRepository.Get(x => x.OrderDetailId == id);

            return obj;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public IEnumerable<OrderDetail> GetByOrder(int OrderId)
        {
            var query = from detail in modelRepository.Get()
                        join product in productRepository.Get() on detail.ProductId equals product.ProductId
                        where detail.OrderId == OrderId
                        select new OrderDetail()
                        {
                            OrderDetailId = detail.OrderDetailId,
                            OrderId = detail.OrderId,
                            ProductId = detail.ProductId,
                            AdditionalDiscount = product.Discount,
                            CreatedBy = detail.CreatedBy,
                            ModifiedAt = detail.ModifiedAt,
                            ModifiedBy = detail.ModifiedBy,
                            Quantity = detail.Quantity,
                            ProductName = product.Title,
                            Price = (int)product.Price,
                            Amount = (float)((product.Price-(product.Price * (0.15))- (product.Price * ((float)product.Discount/100)))* detail.Quantity)
                        };
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public IEnumerable<dynamic> GetByMultipleOrder(List<int> OrderIds)
        {
            var query = from detail in modelRepository.Get()
                        join product in productRepository.Get() on detail.ProductId equals product.ProductId into result
                        from product in result.DefaultIfEmpty()
                        where OrderIds.Contains((int)detail.OrderId)
                        group new { product, detail } by new 
                        { product.ProductId, product.Title } into g
                        select new
                        {
                            ProductName =g.Key.Title,
                            Quantity= g.Sum(x=>x.detail.Quantity)
                        };
            if (query.Any())
            {
                return query.ToList();
            }
            return null;
        }

        public long Create(OrderDetail objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.OrderDetailId;
        }

        public void Update(OrderDetail objModel)
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
    public interface IOrderDetailService : IService
    {
        OrderDetail GetById(long id);
        IEnumerable<OrderDetail> GetAll();
        IEnumerable<OrderDetail> GetByOrder(int OrderId);
        IEnumerable<dynamic> GetByMultipleOrder(List<int> OrderId);
        long Create(OrderDetail objModel);
        void Update(OrderDetail objModel);
        void Delete(int id);
    }
}

