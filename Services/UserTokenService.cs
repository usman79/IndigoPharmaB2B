using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserTokenService(IUserTokenRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public UserToken GetById(long id)
        {
            var obj = modelRepository.Get(x => x.TokenId == id);

            return obj;
        }

        public UserToken GetByAuthToken(string auth_token)
        {
            var obj = modelRepository.Get(x => x.AuthToken == auth_token);
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        public IEnumerable<UserToken> GetAll(long company_account_id)
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(UserToken objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.TokenId;
        }

        public void Update(UserToken objModel)
        {
            modelRepository.Update(objModel);
            Commit();
        }

        public void Delete(long id)
        {
            modelRepository.Delete(id);
            Commit();
        }

        public void DeleteByUserId(long user_id)
        {
            modelRepository.Delete(x => x.UserId == user_id);
            Commit();
        }

        public void DeleteByUserIds(long[] user_ids)
        {
            modelRepository.Delete(x => user_ids.Contains(x.UserId ?? 0));
            Commit();
        }

        public void DeleteByAuthToken(string auth_token)
        {
            modelRepository.Delete(x => x.AuthToken == auth_token);
            Commit();
        }

        public void Commit()
        {
            unitOfWork.Commit();
        }
    }

    public interface IUserTokenService : IService
    {
        UserToken GetById(long id);
        UserToken GetByAuthToken(string auth_token);
        IEnumerable<UserToken> GetAll(long company_account_id);
        long Create(UserToken objModel);
        void Update(UserToken objModel);
        void Delete(long id);
        void DeleteByUserId(long user_id);
        void DeleteByUserIds(long[] user_ids);
        void DeleteByAuthToken(string auth_token);
    }
}
