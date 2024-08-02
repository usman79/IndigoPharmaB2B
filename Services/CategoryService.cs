 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public Category GetById(long id)
        {
            var obj = modelRepository.Get(x => x.CategoryId == id);

            return obj;
        }

        public IEnumerable<Category> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(Category objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.CategoryId;
        }

        public void Update(Category objModel)
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
    public interface ICategoryService : IService
    {
        Category GetById(long id);
        IEnumerable<Category> GetAll();
        long Create(Category objModel);
        void Update(Category objModel);
        void Delete(int id);
    }
}
