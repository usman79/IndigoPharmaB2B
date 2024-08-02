 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public PermissionService(IPermissionRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public Permission GetById(long id)
        {
            var obj = modelRepository.Get(x => x.PermissionId == id);

            return obj;
        }

        public IEnumerable<Permission> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(Permission objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.PermissionId;
        }

        public void Update(Permission objModel)
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
    public interface IPermissionService : IService
    {
        Permission GetById(long id);
        IEnumerable<Permission> GetAll();
        long Create(Permission objModel);
        void Update(Permission objModel);
        void Delete(long id);
    }
}
