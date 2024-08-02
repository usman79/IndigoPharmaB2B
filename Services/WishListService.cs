using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 
 


namespace IndigoAdmin.Services
{
    public class WishListService:IWishListService
    {
        private readonly IWishListRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public WishListService(IWishListRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<WishList> GetById(int  id)
        {
            var obj = modelRepository.GetMany(x => x.UserId == id);

            if (obj.Any())
            {
                return obj.ToList();
            }
            return null;
        }

        public IEnumerable<WishList> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(WishList objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.Id;
        }

        public void Update(WishList objModel)
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
    public interface IWishListService : IService
    {
        IEnumerable<WishList> GetById(int id);
        IEnumerable<WishList> GetAll();
        long Create(WishList objModel);
        void Update(WishList objModel);
        void Delete(int id);
    }
}
