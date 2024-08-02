 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public BrandService(IBrandRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public Brand  GetById(long id)
        {
            var obj = modelRepository.Get(x => x.BrandId == id);

            return obj;
        }

        public IEnumerable<Brand> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(Brand objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.BrandId;
        }

        public void Update(Brand objModel)
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
    public interface IBrandService : IService
    {
        Brand GetById(long id);
        IEnumerable<Brand> GetAll();
        long Create(Brand objModel);
        void Update(Brand objModel);
        void Delete(int id);
    }
}
