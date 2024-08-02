using Microsoft.AspNetCore.Mvc;
using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.Models;
using IndigoAdmin.Services;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IndigoAdmin.Controllers
{
    public class TransactionController : Controller
    {
        ITransactionService _TransactionService;
        IInventoryService _InventoryService;

        public TransactionController(ITransactionService TransactionService, IInventoryService InventoryService)
        {
            _TransactionService = TransactionService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllBrands()
        {
            var listCompanies = _TransactionService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(Transaction objModal)
        {
            try
            {
                if (objModal.TransactionId == 0)
                {
                    var TransactionId = _TransactionService.Create(objModal);
                }
                else
                {
                    var oldData = _TransactionService.GetById(objModal.TransactionId);
                    if (oldData != null)
                    {
                        oldData.TransactionId = objModal.TransactionId;
                        oldData.OrderId= objModal.OrderId;
                        oldData.UserId = objModal.UserId;
                        oldData.TotalAmount = objModal.TotalAmount;
                        oldData.PaidAmount = objModal.PaidAmount;
                        oldData.Status = objModal.Status;
                        oldData.DueDate = objModal.DueDate;


                        // oldData.Image= ;

                        oldData.ModifiedAt = DateTime.Now;

                    }
                    _TransactionService.Update(oldData);
                }
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                _TransactionService.Delete(Id);
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }

        [HttpGet]
        public JsonResult DateWiseOrders(DateTime StartDate, DateTime EndDate,int UserId)
        {
            var listData = _TransactionService.GetDateWise(StartDate, EndDate, UserId);

            return Json(listData, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
    }
}


 






