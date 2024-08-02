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
    public class PermissionController : Controller
    {
        IPermissionService _PermissionService;
        IInventoryService _InventoryService;

        public PermissionController(IPermissionService PermissionService, IInventoryService InventoryService)
        {
            _PermissionService = PermissionService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllBrands()
        {
            var listCompanies = _PermissionService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(Permission objModal)
        {
            try
            {
                if (objModal.PermissionId == 0)
                {
                    var PermissionId = _PermissionService.Create(objModal);
                }
                else
                {
                    var oldData = _PermissionService.GetById(objModal.PermissionId);
                    if (oldData != null)
                    {
                        oldData.PermissionName = objModal.PermissionName;
                       

                        oldData.PermissionId = objModal.PermissionId;
                      

                         

                    }
                    _PermissionService.Update(oldData);
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
                _PermissionService.Delete(Id);
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


 



