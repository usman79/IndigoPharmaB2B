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
    public class OrderDetailController : Controller
    {
        IOrderDetailService _OrderDetailService;
        IOrderService _OrderService;
        IInventoryService _InventoryService;

        public OrderDetailController(IOrderDetailService OrderDetailService, IOrderService OrderService, IInventoryService InventoryService)
        {
            _OrderDetailService = OrderDetailService;
            _OrderService = OrderService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllDetailes()
        {
            var listCompanies = _OrderDetailService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
        [HttpGet]
        public JsonResult GetByOrder(int OrderId)
        {
            var listCompanies = _OrderDetailService.GetByOrder(OrderId);

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(OrderDetail objModal)
        {
            try
            {
                if (objModal.OrderDetailId == 0)
                {
                    var OrderDetailId = _OrderDetailService.Create(objModal);
                }
                else
                {
                    var oldData = _OrderDetailService.GetById(objModal.OrderDetailId);
                    if (oldData != null)
                    {
                        oldData.OrderDetailId = objModal.OrderDetailId;
                        oldData.OrderId = objModal.OrderId;
                        oldData.ProductId = objModal.ProductId;
                        oldData.Quantity = objModal.Quantity;

                         

                        oldData.ModifiedAt = DateTime.Now;
                    }
                    _OrderDetailService.Update(oldData);
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
        public ActionResult GetByOrderIds([FromBody]List<int> OrderIdList)
        {
            try
            {
                var listCompanies = _OrderDetailService.GetByMultipleOrder(OrderIdList);
                return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost]
        public ActionResult SaveBulk([FromBody]OrderDetailList objModalList)
        {
            try
            {
                int OrderId = objModalList.OrderId;
                var ExistingOrderDetails = _OrderDetailService.GetByOrder(OrderId);
                foreach(var item in ExistingOrderDetails)
                {
                    var temp = objModalList.Details.Where(x => x.OrderDetailId == item.OrderDetailId).FirstOrDefault();
                    if (temp != null)
                    {
                        _OrderDetailService.Update(temp);
                    }
                    else
                    {
                        _OrderDetailService.Delete(item.OrderDetailId);
                    }
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
                _OrderDetailService.Delete(Id);
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




 
 


 


