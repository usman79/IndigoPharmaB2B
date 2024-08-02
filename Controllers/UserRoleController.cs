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
    public class UserRoleController : Controller
    {
        IUserRoleService _UserRoleService;
        IInventoryService _InventoryService;

        public UserRoleController(IUserRoleService UserRoleService, IInventoryService InventoryService)
        {
            _UserRoleService = UserRoleService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllBrands()
        {
            var listCompanies = _UserRoleService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(UserRole objModal)
        {
            try
            {
                if (objModal.UserRoleId == 0)
                {
                    var UserRoleId = _UserRoleService.Create(objModal);
                }
                else
                {
                    var oldData = _UserRoleService.GetById(objModal.UserRoleId);
                    if (oldData != null)
                    {
                        oldData.UserRoleId = objModal.UserRoleId;
                        oldData.UserRoleName = objModal.UserRoleName;
                        oldData.ActiveStatus = objModal.ActiveStatus;
                       


 



                    }
                    _UserRoleService.Update(oldData);
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
                _UserRoleService.Delete(Id);
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


 












