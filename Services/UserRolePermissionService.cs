 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class UserRolePermissionService : IUserRolePermissionService
    {
        private readonly IUserRolePermissionRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserRolePermissionService(IUserRolePermissionRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public UserRolePermission GetById(long id)
        {
            var obj = modelRepository.Get(x => x.UserRolePermissionId == id);

            return obj;
        }

        public IEnumerable<UserRolePermission> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(UserRolePermission objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.UserRolePermissionId;
        }

        public void Update(UserRolePermission objModel)
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
    public interface IUserRolePermissionService : IService
    {
        UserRolePermission GetById(long id);
        IEnumerable<UserRolePermission> GetAll();
        long Create(UserRolePermission objModel);
        void Update(UserRolePermission objModel);
        void Delete(long id);
    }
}
