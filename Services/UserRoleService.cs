 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserRoleService(IUserRoleRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public UserRole GetById(long id)
        {
            var obj = modelRepository.Get(x => x.UserRoleId == id);

            return obj;
        }

        public IEnumerable<UserRole> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(UserRole objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.UserRoleId;
        }

        public void Update(UserRole objModel)
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
    public interface IUserRoleService : IService
    {
        UserRole GetById(long id);
        IEnumerable<UserRole> GetAll();
        long Create(UserRole objModel);
        void Update(UserRole objModel);
        void Delete(long id);
    }
}
