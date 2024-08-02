
using IndigoAdmin.DAL.Data.Infrastructure;
using IndigoAdmin.DAL.Data.Repositories;
using IndigoAdmin.DAL.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using IndigoAdmin.Models;

namespace IndigoAdmin.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository modelRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly IUserAccountRepository userRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IUnitOfWork unitOfWork;

        IOrderDetailService _OrderDetailService;
        public OrderService(IOrderRepository modelRepository, IOrderDetailRepository orderDetailRepository, IInventoryRepository inventoryRepository, IUserAccountRepository userRepository, IOrderDetailService OrderDetailService, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            this.modelRepository = modelRepository;
            this.userRepository = userRepository;
            this.transactionRepository = transactionRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.inventoryRepository = inventoryRepository;
            this.unitOfWork = unitOfWork;

            _OrderDetailService = OrderDetailService;
        }

        public Order GetById(long id)
        {
            var obj = modelRepository.Get(x => x.OrderId == id);

            return obj;
        }

        public IEnumerable<Order> GetAll()
        {
            var debitTransactions = from transaction in transactionRepository.Get()
                                    where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3)
                                    select transaction;
            var creditTransactions = from transaction in transactionRepository.Get()
                                     where (transaction.TransactionTypeId == 1)
                                     select transaction;
            var query = from order in modelRepository.Get()
                        join user in userRepository.Get() on order.UserId equals (int)user.UserId into result
                        from user in result.DefaultIfEmpty()
                        select new Order()
                        {
                            OrderId = order.OrderId,
                            BillingAddress = order.BillingAddress,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            Latitude = order.Latitude,
                            Logitude = order.Logitude,
                            ModifiedAt = order.ModifiedAt,
                            ModifiedBy = order.ModifiedBy,
                            OrderTakerId = order.OrderTakerId,
                            PaymentType = order.PaymentType,
                            Status = order.Status,
                            UserId = order.UserId,
                            PaidAmount = debitTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            TotalAmount = creditTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            UserName = (order.OrderType == 1) ? user.UserFirstName + " " + user.UserLastName : order.UserName
                        };
            var objs = query.ToList();
            //if (objs.Any())
            //{
            //    //foreach (var item in objs)
            //    //{
            //    //    var details = _OrderDetailService.GetByOrder(item.OrderId);
            //    //    if (details != null)
            //    //    {
            //    //        item.TotalAmount = details.Sum(x => x.Amount);
            //    //    }
            //    //    var transactions = from transaction in transactionRepository.Get()
            //    //                       where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3) && transaction.OrderId == item.OrderId
            //    //                       select transaction;
            //    //    item.PaidAmount = transactions.Sum(x => x.TotalAmount) ?? 0;
            //    //}
            //    return objs;
            //} 
            return objs;
        }

        public IEnumerable<Order> GetDateWise(DateTime StartDate, DateTime EndDate)
        {
            var debitTransactions = from transaction in transactionRepository.Get()
                                    where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3)
                                    select transaction;
            var creditTransactions = from transaction in transactionRepository.Get()
                                     where (transaction.TransactionTypeId == 1)
                                     select transaction;
            var query = from order in modelRepository.Get()
                        join user in userRepository.Get() on order.UserId equals (int)user.UserId into result
                        from user in result.DefaultIfEmpty()
                        where order.CreatedAt >= StartDate && order.CreatedAt <= EndDate
                        select new Order()
                        {
                            OrderId = order.OrderId,
                            BillingAddress = order.BillingAddress,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            Latitude = order.Latitude,
                            Logitude = order.Logitude,
                            ModifiedAt = order.ModifiedAt,
                            ModifiedBy = order.ModifiedBy,
                            OrderTakerId = order.OrderTakerId,
                            PaymentType = order.PaymentType,
                            Status = order.Status,
                            UserId = order.UserId,
                            PaidAmount = debitTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            TotalAmount = creditTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            UserName = (order.OrderType == 1) ? user.UserFirstName + " " + user.UserLastName : order.UserName
                        };
            var objs = query.ToList();
            //if (objs.Any())
            //{
            //    //foreach (var item in objs)
            //    //{
            //    //    var details = _OrderDetailService.GetByOrder(item.OrderId);
            //    //    if (details != null)
            //    //    {
            //    //        item.TotalAmount = details.Sum(x => x.Amount);
            //    //    }
            //    //    var transactions = from transaction in transactionRepository.Get()
            //    //                       where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3) && transaction.OrderId == item.OrderId
            //    //                       select transaction;
            //    //    item.PaidAmount = transactions.Sum(x => x.TotalAmount) ?? 0;
            //    //}
            //    return objs;
            //}
            return objs;
        }

        public IEnumerable<Order> GetAllByUser(int UserId)
        {
            var debitTransactions = from transaction in transactionRepository.Get()
                                    where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3)
                                    select transaction;
            var creditTransactions = from transaction in transactionRepository.Get()
                                     where (transaction.TransactionTypeId == 1)
                                     select transaction;
            var query = from order in modelRepository.Get()
                        join user in userRepository.Get() on order.UserId equals (int)user.UserId
                        where user.UserId == UserId
                        select new Order()
                        {
                            OrderId = order.OrderId,
                            BillingAddress = order.BillingAddress,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            Latitude = order.Latitude,
                            Logitude = order.Logitude,
                            ModifiedAt = order.ModifiedAt,
                            ModifiedBy = order.ModifiedBy,
                            OrderTakerId = order.OrderTakerId,
                            PaymentType = order.PaymentType,
                            Status = order.Status,
                            UserId = order.UserId,
                            PaidAmount = debitTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            TotalAmount = creditTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            UserName = (order.OrderType == 1) ? user.UserFirstName + " " + user.UserLastName : order.UserName
                        };
            var objs = query.ToList();
            //if (objs.Any())
            //{
            //    foreach (var item in objs)
            //    {
            //        item.TotalAmount = _OrderDetailService.GetByOrder(item.OrderId).Sum(x => x.Amount);
            //    }
            //    return objs;
            //}
            return objs;
        }


        public IEnumerable<Order> GetAllByOt(int Id)
        {
            var debitTransactions = from transaction in transactionRepository.Get()
                                    where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3)
                                    select transaction;
            var creditTransactions = from transaction in transactionRepository.Get()
                                     where (transaction.TransactionTypeId == 1)
                                     select transaction;
            var query = from order in modelRepository.Get()
                        join user in userRepository.Get() on order.OrderTakerId equals (int)user.UserId
                        where user.UserId == Id
                        select new Order()
                        {
                            OrderId = order.OrderId,
                            BillingAddress = order.BillingAddress,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            Latitude = order.Latitude,
                            Logitude = order.Logitude,
                            ModifiedAt = order.ModifiedAt,
                            ModifiedBy = order.ModifiedBy,
                            OrderTakerId = order.OrderTakerId,
                            PaymentType = order.PaymentType,
                            Status = order.Status,
                            UserId = order.UserId,
                            PaidAmount = debitTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            TotalAmount = creditTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            UserName = (order.OrderType == 1) ? user.UserFirstName + " " + user.UserLastName : order.UserName
                        };
            var objs = query.ToList();
            //if (objs.Any())
            //{
            //    foreach (var item in objs)
            //    {
            //        item.TotalAmount = _OrderDetailService.GetByOrder(item.OrderId).Sum(x => x.Amount);
            //    }
            //    return objs;
            //}
            return objs;
        }
        public IEnumerable<Order> GetAllByDeliveryBoy(int Id)
        {
            var debitTransactions = from transaction in transactionRepository.Get()
                                    where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3)
                                    select transaction;
            var creditTransactions = from transaction in transactionRepository.Get()
                                     where (transaction.TransactionTypeId == 1)
                                     select transaction;
            var query = from order in modelRepository.Get()
                        join user in userRepository.Get() on order.OrderTakerId equals (int)user.UserId
                        where user.AssignedDeliveryBoyId == Id
                        select new Order()
                        {
                            OrderId = order.OrderId,
                            BillingAddress = order.BillingAddress,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            Latitude = order.Latitude,
                            Logitude = order.Logitude,
                            ModifiedAt = order.ModifiedAt,
                            ModifiedBy = order.ModifiedBy,
                            OrderTakerId = order.OrderTakerId,
                            PaymentType = order.PaymentType,
                            Status = order.Status,
                            UserId = order.UserId,
                            PaidAmount = debitTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            TotalAmount = creditTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            UserName = (order.OrderType == 1) ? user.UserFirstName + " " + user.UserLastName : order.UserName
                        };
            var objs = query.ToList();
            //if (objs.Any())
            //{
            //    foreach (var item in objs)
            //    {
            //        item.TotalAmount = _OrderDetailService.GetByOrder(item.OrderId).Sum(x => x.Amount);
            //    }
            //    return objs;
            //}
            return objs;
        }
        public IEnumerable<Order> GetAllByDeliveryBoy(int Id, DateTime StartDate, DateTime EndDate)
        {
            var orders = from order in modelRepository.Get() select order;
            if (Id != 0)
            {
                //orders = orders.Where(order => order.DeliveryBoyId == Id);
            }
            if (StartDate != null)
            {
                orders = orders.Where(order => order.CreatedAt >= StartDate);
            }
            if (EndDate != null)
            {
                orders = orders.Where(order => order.CreatedAt <= EndDate);
            }
            var orderData = orders.ToList();
            var debitTransactions = from transaction in transactionRepository.Get()
                                    where (transaction.TransactionTypeId == 2 || transaction.TransactionTypeId == 3)
                                    select transaction;
            var creditTransactions = from transaction in transactionRepository.Get()
                                     where (transaction.TransactionTypeId == 1)
                                     select transaction;
            var query = from order in orders
                        join user in userRepository.Get() on order.UserId equals (int)user.UserId
                        where user.AssignedDeliveryBoyId == Id
                        select new Order()
                        {
                            OrderId = order.OrderId,
                            BillingAddress = order.BillingAddress,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            Latitude = order.Latitude,
                            Logitude = order.Logitude,
                            ModifiedAt = order.ModifiedAt,
                            ModifiedBy = order.ModifiedBy,
                            OrderTakerId = order.OrderTakerId,
                            PaymentType = order.PaymentType,
                            Status = order.Status,
                            UserId = order.UserId,
                            PaidAmount = debitTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            TotalAmount = creditTransactions.Where(x => x.OrderId == order.OrderId).Sum(x => x.TotalAmount) ?? 0,
                            UserName = (order.OrderType == 1) ? user.UserFirstName + " " + user.UserLastName : order.UserName
                        };
            var objs = query.ToList();
            //if (objs.Any())
            //{
            //    foreach (var item in objs)
            //    {
            //        item.TotalAmount = _OrderDetailService.GetByOrder(item.OrderId).Sum(x => x.Amount);
            //    }
            //    return objs;
            //}
            return objs;
        }

        public IEnumerable<SalesmanReportModel> GetDateWiseByOt(int Id, DateTime StartDate, DateTime EndDate)
        {
            try
            {

                var orders = from order in modelRepository.Get() select order;
                if (Id != 0)
                {
                    orders = orders.Where(order => order.OrderTakerId == Id);
                }
                if (StartDate != null)
                {
                    orders = orders.Where(order => order.CreatedAt >= StartDate);
                }
                if (EndDate != null)
                {
                    orders = orders.Where(order => order.CreatedAt <= EndDate);
                }
                var query = from order in orders
                            join user in userRepository.Get() on order.OrderTakerId equals (int)user.UserId
                            select new SalesmanReportModel()
                            {
                                OrderId = order.OrderId,
                                Name = user.UserFirstName + " " + user.UserLastName,
                            };

                var objs = query.ToList();
                foreach (var item in objs)
                {
                    var details = _OrderDetailService.GetByOrder(item.OrderId);
                    if (details != null)
                    {
                        item.SalesAmount = details.Sum(x => x.Amount);
                        foreach (var det in details)
                        {
                            var inventoryQuery = from inv in inventoryRepository.Get()
                                                 where inv.ProductId == det.ProductId && inv.PurchasePrice != null
                                                 select inv;
                            var inventoryObj = inventoryQuery.FirstOrDefault();
                            if (inventoryObj != null)
                            {
                                item.PurchaseAmount += (float)(inventoryObj.PurchasePrice * det.Quantity);
                            }
                        }
                    }
                }

                var groupQuery = from orderRecord in objs
                                 group orderRecord by orderRecord.Name into g
                                 select new SalesmanReportModel()
                                 {
                                     Name = g.Key,
                                     PurchaseAmount = g.Sum(x => x.PurchaseAmount),
                                     SalesAmount = g.Sum(x => x.SalesAmount)
                                 };

                return groupQuery.ToList();
            }
            catch(Exception e)
            {
                return null;
            }
            return null;
        }

        public int GetCount()
        {
            GetMonthWiseCount();
            var objs = modelRepository.Get().Count();
            return objs;
        }

        public List<dynamic> GetMonthWiseCount()
        {
            List<dynamic> MonthList = new List<dynamic>();
            dynamic DyObj = new ExpandoObject();
            DyObj.Number = 1; DyObj.Title = "Jan"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 2; DyObj.Title = "Fab"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 3; DyObj.Title = "Mar"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 4; DyObj.Title = "Apr"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 5; DyObj.Title = "May"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 6; DyObj.Title = "Jun"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 7; DyObj.Title = "Jul"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 8; DyObj.Title = "Aug"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 9; DyObj.Title = "Sep"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 10; DyObj.Title = "Oct"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 11; DyObj.Title = "Nov"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            DyObj = new ExpandoObject();
            DyObj.Number = 12; DyObj.Title = "Dec"; DyObj.Count = 0;
            MonthList.Add(DyObj);
            var query = from order in modelRepository.Get()
                        where order.CreatedAt.Value.Year == DateTime.Now.Year
                        group order by new
                        {
                            Month = order.CreatedAt.Value.Month
                        } into g
                        select new
                        {
                            Month = g.Key.Month,
                            Count = g.Count()
                        };
            var list = query.ToList();
            foreach (var item in MonthList)
            {
                item.Count = list.Where(x => x.Month == item.Number).FirstOrDefault()?.Count;
                item.Count = (item.Count == null) ? 0 : item.Count;
            }
            return MonthList;
        }

        public long Create(Order objModel)
        {
            modelRepository.Insert(objModel);
            Commit();

            return objModel.OrderId;
        }

        public void Update(Order objModel)
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
    public interface IOrderService : IService
    {
        Order GetById(long id);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetAllByDeliveryBoy(int Id);
        IEnumerable<Order> GetAllByDeliveryBoy(int Id, DateTime StartDate, DateTime EndDate);
        IEnumerable<Order> GetDateWise(DateTime StartDate, DateTime EndDate);
        IEnumerable<Order> GetAllByUser(int UserId);
        IEnumerable<Order> GetAllByOt(int Id);

        IEnumerable<SalesmanReportModel> GetDateWiseByOt(int Id, DateTime StartDate, DateTime EndDate);
        int GetCount();
        List<dynamic> GetMonthWiseCount();
        long Create(Order objModel);
        void Update(Order objModel);
        void Delete(long id);
    }
}
