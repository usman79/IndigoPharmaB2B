 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public SupplierService(ISupplierRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public Supplier GetById(long id)
        {
            var obj = modelRepository.Get(x => x.SupplierId == id);

            return obj;
        }

        public IEnumerable<Supplier> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(Supplier objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.SupplierId;
        }

        public void Update(Supplier objModel)
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
    public interface ISupplierService : IService
    {
        Supplier GetById(long id);
        IEnumerable<Supplier> GetAll();
        long Create(Supplier objModel);
        void Update(Supplier objModel);
        void Delete(long id);
    }
}
