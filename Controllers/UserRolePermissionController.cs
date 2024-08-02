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
    public class UserRolePermissionController : Controller
    {
        IUserRolePermissionService _UserRolePermissionService;
        IInventoryService _InventoryService;

        public UserRolePermissionController(IUserRolePermissionService UserRolePermissionService, IInventoryService InventoryService)
        {
            _UserRolePermissionService = UserRolePermissionService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllPermissions()
        {
            var listCompanies = _UserRolePermissionService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(UserRolePermission objModal)
        {
            try
            {
                if (objModal.UserRolePermissionId == 0)
                {
                    var UserRolePermissionId = _UserRolePermissionService.Create(objModal);
                }
                else
                {
                    var oldData = _UserRolePermissionService.GetById(objModal.UserRolePermissionId);
                    if (oldData != null)
                    {
                        oldData.UserRolePermissionId = objModal.UserRolePermissionId;
                        oldData.UserRoleId = objModal.UserRoleId;
                        oldData.PermissionId = objModal.PermissionId;
                       


                    }
                    _UserRolePermissionService.Update(oldData);
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
                _UserRolePermissionService.Delete(Id);
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

 












