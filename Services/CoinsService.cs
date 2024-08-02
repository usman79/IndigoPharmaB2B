using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

 


 
 


namespace IndigoAdmin.Services
{
    public class CoinsService : ICoinsService
    {
        private readonly ICoinsRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public CoinsService(ICoinsRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Coins> GetById(int id)
        {
            var obj = modelRepository.GetMany(x => x.UserId == id);

            if (obj.Any())
            {
                return obj.ToList();
            }
            return null;
        }
        public Coins GetByUserId(int id)
        {
            var obj = modelRepository.Get(x => x.UserId == id);

            return obj;
        }

        public IEnumerable<Coins> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(Coins objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.Id;
        }

        public void Update(Coins objModel)
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
    public interface ICoinsService : IService
    {
        IEnumerable<Coins> GetById(int id);
        Coins GetByUserId(int id);
        IEnumerable<Coins> GetAll();
        long Create(Coins objModel);
        void Update(Coins objModel);
        void Delete(int id);
    }
}

