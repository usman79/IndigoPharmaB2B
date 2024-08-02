 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class LicenseInformationService : ILicenseInformationService
    {
        private readonly ILicenseInformationRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public LicenseInformationService(ILicenseInformationRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public LicenseInformation GetById(long id)
        {
            var obj = modelRepository.Get(x => x.LicenseId == id);

            return obj;
        }

        public LicenseInformation GetByUserId(long id)
        {
            var obj = modelRepository.Get(x => x.UserId == id);

            return obj;
        }

        public IEnumerable<LicenseInformation> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(LicenseInformation objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.LicenseId;
        }

        public void Update(LicenseInformation objModel)
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
    public interface ILicenseInformationService : IService
    {
        LicenseInformation GetById(long id);
        LicenseInformation GetByUserId(long id);
        IEnumerable<LicenseInformation> GetAll();
        long Create(LicenseInformation objModel);
        void Update(LicenseInformation objModel);
        void Delete(long id);
    }
}
