 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class GeneralExpenseService : IGeneralExpenseService
    {
        private readonly IGeneralExpenseRepository modelRepository;
        private readonly IUnitOfWork unitOfWork;

        public GeneralExpenseService(IGeneralExpenseRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        public GeneralExpense GetById(long id)
        {
            var obj = modelRepository.Get(x => x.ExpenseId == id);

            return obj;
        }

        public IEnumerable<GeneralExpense> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }

        public long Create(GeneralExpense objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.ExpenseId;
        }

        public void Update(GeneralExpense objModel)
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
    public interface IGeneralExpenseService : IService
    {
        GeneralExpense GetById(long id);
        IEnumerable<GeneralExpense> GetAll();
        long Create(GeneralExpense objModel);
        void Update(GeneralExpense objModel);
        void Delete(long id);
    }
}
