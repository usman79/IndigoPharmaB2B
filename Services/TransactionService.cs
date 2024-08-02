 
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository modelRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IUserAccountRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public TransactionService(ITransactionRepository modelRepository, IOrderRepository orderRepository, IUserAccountRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.orderRepository = orderRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public Transaction GetById(long id)
        {
            var obj = modelRepository.Get(x => x.TransactionId == id);

            return obj;
        }
        public Transaction GetByOrderId(int OrderId, int Type)
        {
            var obj = modelRepository.Get(x => x.OrderId == OrderId && x.TransactionTypeId==Type);

            return obj;
        }

        public IEnumerable<Transaction> GetAll()
        {
            var objs = modelRepository.GetAll();
            if (objs.Any())
            {
                return objs.ToList();
            }
            return null;
        }
        
        public float GetCreditAmount(int OrderId)
        {
            var CreditAmount = modelRepository.GetMany(x => x.OrderId == OrderId && x.TransactionTypeId==1).Sum(x => x.TotalAmount);
            var DeditAmount = modelRepository.GetMany(x => x.OrderId == OrderId && (x.TransactionTypeId == 2 || x.TransactionTypeId == 3)).Sum(x => x.TotalAmount);
            return (float)(CreditAmount - DeditAmount);
        }

        public IEnumerable<Transaction> GetDateWise(DateTime StartDate, DateTime EndDate, int UserId)
        {
            var Transactions = from transaction in modelRepository.Get()
                               join order in orderRepository.Get() on transaction.OrderId equals order.OrderId
                               where order.CreatedAt >= StartDate && order.CreatedAt <= EndDate &&  order.UserId == UserId
                               select transaction;
            return Transactions.ToList();
        }

        public long Create(Transaction objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.TransactionId;
        }

        public void Update(Transaction objModel)
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
    public interface ITransactionService : IService
    {
        Transaction GetById(long id);
        Transaction GetByOrderId(int OrderId,int Type);
        IEnumerable<Transaction> GetAll();
        float GetCreditAmount(int OrderId);
        IEnumerable<Transaction> GetDateWise(DateTime StartDate, DateTime EndDate, int UserId);
        long Create(Transaction objModel);
        void Update(Transaction objModel);
        void Delete(long id);
    }
}
