using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IndigoAdmin.Controllers
{
    public class OrderTakerController : Controller
    {
        IUserAccountService _UserAccountService;
        IOrderService _OrderService;

        public OrderTakerController(IUserAccountService UserAccountService, IOrderService OrderService)
        {
            _UserAccountService = UserAccountService;
            _OrderService = OrderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllOrderTakers()
        {
            var listCompanies = _UserAccountService.GetAllByRole(2);

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(UserAccount objModal)
        {
            try
            {
                if (objModal.UserId == 0)

                {
                    objModal.UserRoleId = 2;
                    objModal.IsPasswordReset = true;
                    objModal.ActiveStatus = true;
                    var BrandId = _UserAccountService.Create(objModal);
                }
                else
                {
                    var oldModal = _UserAccountService.GetById(objModal.UserId);
                    if (oldModal != null)
                    {
                        oldModal.UserFirstName = objModal.UserFirstName;
                        oldModal.UserLastName = objModal.UserLastName;
                        oldModal.UserEmailAddress = objModal.UserEmailAddress;
                        oldModal.Address = objModal.Address;
                        oldModal.UserPhone = objModal.UserPhone;
                        _UserAccountService.Update(oldModal);
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
                _UserAccountService.Delete(Id);
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }

        [HttpGet]
        public JsonResult UpdateStatus(int UserId, bool Status)
        {
            var user = _UserAccountService.GetById(UserId);

            if (user != null)
            {
                user.ActiveStatus = Status;
                _UserAccountService.Update(user);

                return Json(true, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
            }

            return Json(false, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
    }
}
