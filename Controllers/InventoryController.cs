using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.Helpers;
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
    public class InventoryController : Controller
    {
        IInventoryService _InventoryService;
        IProductService _PrdocutService;

        public InventoryController(IInventoryService InventoryService, IProductService PrdocutService)
        {
            _InventoryService = InventoryService;
            _PrdocutService = PrdocutService;
        }
        public IActionResult Index()
        {
            ViewBag.UserId = 0;
            return View();
        }

        public IActionResult SuplierPurchase(int Id)
        {
            ViewBag.UserId = Id;
            return View("Index");
        }

        [HttpGet]
        public JsonResult GetAllInventory()
        {
            var listCompanies = _InventoryService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpGet]
        public JsonResult GetAllInventoryByUser(int Id)
        {
            var listCompanies = _InventoryService.GetAllBySupplier(Id);

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save([FromBody] List<Inventory> objModalList)
        {
            try
            {
                if (objModalList.Count>0)
                {
                    foreach(var objModal in objModalList)
                    {
                        objModal.CreatedAt = DateTime.Now;
                        objModal.CreatedBy = 1;
                        var ExpenseId = _InventoryService.Create(objModal);
                        var product = _PrdocutService.GetById((int)objModal.ProductId);
                        if (product != null)
                        {
                            if (product.Quantity != null)
                                product.Quantity += objModal.Quantity;
                            else
                                product.Quantity = objModal.Quantity;

                            _PrdocutService.Update(product);
                        }
                    }

                }
                else
                {
                    //var oldData = _InventoryService.GetById(objModal.InventoryId);
                    //if (oldData != null)
                    //{
                    //    oldData.ProductId = objModal.ProductId;
                    //    oldData.Quantity = objModal.Quantity;
                    //    oldData.Price = objModal.Price;
                    //    oldData.SupplierId = objModal.SupplierId;

                    //    oldData.ModifiedAt = DateTime.Now;
                    //    objModal.ModifiedBy = 1;
                    //}
                    //_InventoryService.Update(oldData);
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
        public ActionResult Update(Inventory objModal)
        {
            try
            {
                int quantityDif = 0;
                var oldData = _InventoryService.GetById(objModal.InventoryId);
                if (oldData != null)
                {
                    quantityDif = objModal.Quantity - oldData.Quantity??0;
                    oldData.ProductId = objModal.ProductId;
                    oldData.Quantity = objModal.Quantity;
                    oldData.Price = objModal.Price;
                    oldData.Discount = objModal.Discount;
                    oldData.BatchNumber = objModal.BatchNumber;

                    oldData.ModifiedAt = DateTime.Now;
                    objModal.ModifiedBy = 1;
                }
                _InventoryService.Update(oldData);
                var product = _PrdocutService.GetById((int)objModal.ProductId);
                if (product != null)
                {
                    product.Quantity = product.Quantity+ quantityDif;

                    _PrdocutService.Update(product);
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
                _InventoryService.Delete(Id);
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




