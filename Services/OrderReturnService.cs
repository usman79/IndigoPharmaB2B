
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IndigoAdmin.Services
{
    public class OrderReturnService
    {

        private readonly IOrderReturnRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public OrderReturnService(IOrderReturnRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public OrderReturn GetById(long id)
        {
            var obj = modelRepository.Get(x => x.OrderReturnId == id);

            return obj;
        }

        public IEnumerable<OrderReturn> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(OrderReturn objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.OrderReturnId;
        }

        public void Update(OrderReturn objModel)
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

    public interface IOrderReturnService : IService
    {
        Brand GetById(long id);
        IEnumerable<Brand> GetAll();
        long Create(Brand objModel);
        void Update(Brand objModel);
        void Delete(int id);
    }
}
