
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class ChequeDetailService : IChequeDetailService
    {
        private readonly IChequeDetailRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChequeDetailService(IChequeDetailRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public ChequeDetail GetById(long id)
        {
            var obj = modelRepository.Get(x => x.ChequeDetailId == id);

            return obj;
        }

        public IEnumerable<ChequeDetail> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(ChequeDetail objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.ChequeDetailId;
        }

        public void Update(ChequeDetail objModel)
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
    public interface IChequeDetailService : IService
    {
        ChequeDetail  GetById(long id);
        IEnumerable<ChequeDetail> GetAll();
        long Create(ChequeDetail objModel);
        void Update(ChequeDetail objModel);
        void Delete(int id);
    }
}
