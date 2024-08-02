using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.Models;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IndigoAdmin.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _OrderService;
        IOrderDetailService _OrderDetailService;
        IInventoryService _InventoryService;
        ITransactionService _TransactionService;
        IProductService _PrdocutService;
        IChequeDetailService _ChequeDetailService;

        public OrderController(IOrderService OrderService, IProductService PrdocutService, IOrderDetailService OrderDetailService, IInventoryService InventoryService, ITransactionService TransactionService, IChequeDetailService ChequeDetailService)
        {
            _OrderService = OrderService;
            _OrderDetailService = OrderDetailService;
            _InventoryService = InventoryService;
            _TransactionService = TransactionService;
            _PrdocutService = PrdocutService;
            _ChequeDetailService = ChequeDetailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllOrders()
        {
            var listCompanies = _OrderService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
        
        [HttpGet]
        public JsonResult GetDeliveryBoyOrders(int Id,DateTime StartDate,DateTime EndDate)
        {
            var listCompanies = _OrderService.GetAllByDeliveryBoy(Id,StartDate,EndDate);

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
        
        [HttpGet]
        public JsonResult UpdateStatus(int OrderId,int Status)
        {
            var order = _OrderService.GetById(OrderId);

            if (order != null)
            {
                if (order.Status == 1 && Status == 2)
                {
                    //Add Transaction
                    AddOrderTransaction(order);
                }
                order.Status = Status;
                _OrderService.Update(order);

                return Json(true, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
            }

            return Json(false, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        private void AddOrderTransaction(Order order)
        {
            float amount = 0;
            var details = _OrderDetailService.GetByOrder(order.OrderId);
            if (details != null)
            {
                foreach (var item in details)
                {
                    var ProductDTO = _PrdocutService.GetById((int)item.ProductId);
                    float mrp;
                    float disc;
                    mrp = ProductDTO.Price - ((ProductDTO.Price / 100) * 15) ?? 0;
                    disc = ((mrp / 100) * ProductDTO.Discount) ?? 0;
                    var tempAmount = (mrp - disc) * item.Quantity;
                    amount = (amount + (float)tempAmount);
                }
                Transaction TransactionDTO = new Transaction()
                {
                    CreatedAt = DateTime.Now,
                    OrderId = order.OrderId,
                    TotalAmount = (float)Math.Floor(amount),
                    UserId = order.UserId,
                    TransactionTypeId = 1,//1 for credit
                    Status = 1,
                    Balance = (float)Math.Floor(amount)
                };
                _TransactionService.Create(TransactionDTO);
            }
        }
        private float CalculateTransaction(Order order)
        {
            float amount = 0;
            var details = _OrderDetailService.GetByOrder(order.OrderId);
            if (details != null)
            {
                foreach (var item in details)
                {
                    var ProductDTO = _PrdocutService.GetById((int)item.ProductId);
                    float mrp;
                    float disc;
                    mrp = ProductDTO.Price - ((ProductDTO.Price / 100) * 15) ?? 0;
                    disc = ((mrp / 100) * ProductDTO.Discount) ?? 0;
                    var tempAmount = (mrp - disc) * item.Quantity;
                    amount = (amount + (float)tempAmount);
                }
            }
            return (float)Math.Floor(amount);
        }

        [HttpGet]
        public JsonResult AddPayment(int OrderId, float Payment, int PaymentType)
        {
            var order = _OrderService.GetById(OrderId);

            if (order != null)
            {
                var creditTransactions = _TransactionService.GetCreditAmount(OrderId);
                Transaction TransactionDTO = new Transaction()
                {
                    CreatedAt = DateTime.Now,
                    OrderId = (int)OrderId,
                    TotalAmount = Payment,
                    UserId = order.UserId,
                    TransactionTypeId = PaymentType,//2 for debit
                    Status = 1,
                    Balance= creditTransactions- Payment
                };
                _TransactionService.Create(TransactionDTO);

                return Json(true, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
            }

            return Json(false, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(Order objModal)
        {
            try
            {
                if (objModal.OrderId == 0)
                {
                    var OrdertId = _OrderService.Create(objModal);
                }
                else
                {
                    var oldData = _OrderService.GetById(objModal.OrderId);
                    if (oldData != null)
                    {
                        oldData.OrderId = objModal.OrderId;
                        oldData.UserId = objModal.UserId;
                        oldData.OrderTakerId = objModal.OrderTakerId;
                        oldData.Status = objModal.Status;
                        oldData.PaymentType = objModal.PaymentType;

                        oldData.BillingAddress = objModal.BillingAddress;
                        oldData.Logitude = objModal.Logitude;
                        oldData.Latitude = objModal.Latitude;
                         
                        oldData.ModifiedAt = DateTime.Now;
                    }
                    _OrderService.Update(oldData);
                }
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult SaveProcessedOrder([FromBody] ProcessedOrder objModal)
        {
            try
            {
                if(objModal.OrderId != 0)
                {
                    var order = _OrderService.GetById(objModal.OrderId);
                    //Return Work
                    if (objModal.returnDetails != null && objModal.returnDetails.Count > 0)
                    {
                        bool returnChanges = false;
                        foreach(var item in objModal.returnDetails)
                        {
                            var detailsObject = _OrderDetailService.GetById(item.OrderDetailId);
                            if(detailsObject != null)
                            {
                                if(item.Quantity != 0)
                                {
                                    returnChanges = true;
                                    detailsObject.Quantity = detailsObject.Quantity - item.Quantity;
                                    if (detailsObject.Quantity == 0)
                                    {
                                        _OrderDetailService.Delete(detailsObject.OrderDetailId);
                                    }
                                    else
                                    {
                                        _OrderDetailService.Update(detailsObject);
                                    }
                                }
                            }
                        }
                        if (returnChanges)
                        {
                            var transaction = _TransactionService.GetByOrderId(objModal.OrderId, 1);
                            if (transaction != null)
                            {
                                var newAmount = CalculateTransaction(order);
                                transaction.TotalAmount = newAmount;
                                transaction.Balance = newAmount;
                                _TransactionService.Update(transaction);
                            }
                            else
                            {
                                AddOrderTransaction(order);
                            }
                        }
                    }
                    //Payment Work
                    if (objModal.PaymentType == 1)//Cash
                    {
                        if(objModal.Payment != 0)
                        {
                            var creditTransactions = _TransactionService.GetCreditAmount(objModal.OrderId);
                            Transaction TransactionDTO = new Transaction()
                            {
                                CreatedAt = DateTime.Now,
                                OrderId = objModal.OrderId,
                                TotalAmount = objModal.Payment,
                                UserId = order.UserId,
                                TransactionTypeId = 2,//2 for debit
                                Status = 1,
                                Balance = creditTransactions - objModal.Payment
                            };
                            _TransactionService.Create(TransactionDTO);
                        }
                        if(objModal.Expense != 0)
                        {
                            var creditTransactions = _TransactionService.GetCreditAmount(objModal.OrderId);
                            Transaction TransactionDTO = new Transaction()
                            {
                                CreatedAt = DateTime.Now,
                                OrderId = objModal.OrderId,
                                TotalAmount = objModal.Expense,
                                UserId = order.UserId,
                                TransactionTypeId = 3,//2 for debit
                                Status = 1,
                                Balance = creditTransactions - objModal.Expense
                            };
                            _TransactionService.Create(TransactionDTO);
                        }
                        order.Status = 4;
                        _OrderService.Update(order);
                    }
                    else if(objModal.PaymentType == 2)//Cheque
                    {
                        objModal.chequeDetail.OrderId = objModal.OrderId;
                        _ChequeDetailService.Create(objModal.chequeDetail);
                        order.Status = 4;
                        _OrderService.Update(order);
                    }
                }
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok("Ok");
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                _OrderService.Delete(Id);
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }
    }
}

 
