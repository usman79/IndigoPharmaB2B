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
    public class GeneralExpenseController : Controller
    {

        IGeneralExpenseService _GeneralExpenseService;
        IInventoryService _InventoryService;

        public GeneralExpenseController(IGeneralExpenseService GeneralExpenseService, IInventoryService InventoryService)
        {
            _GeneralExpenseService = GeneralExpenseService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllExpenses()
        {
            var listCompanies = _GeneralExpenseService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(GeneralExpense objModal)
        {
            try
            {
                if (objModal.ExpenseId == 0)
                {
                    var ExpenseId = _GeneralExpenseService.Create(objModal);
                }
                else
                {
                    var oldData = _GeneralExpenseService.GetById(objModal.ExpenseId);
                    if (oldData != null)
                    {
                        oldData.Title = objModal.Title;
                        oldData.Status = objModal.Status;
                        oldData.Amount = objModal.Amount;
                        oldData.ExpenseId = objModal.ExpenseId;
                         
                        oldData.ModifiedAt = DateTime.Now;
                    }
                    _GeneralExpenseService.Update(oldData);
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
                _GeneralExpenseService.Delete(Id);
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


 
 
