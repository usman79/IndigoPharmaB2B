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
    public class UserAccountController : Controller
    {
        IUserAccountService _UserAccountService;

        public UserAccountController(IUserAccountService UserAccountService)
        {
            _UserAccountService = UserAccountService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var listCompanies = _UserAccountService.GetAllByRole(3);

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
        public JsonResult GetAllCustomer()
        {
            var listCompanies = _UserAccountService.GetAllByRole(3);

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
        [HttpGet]
        public JsonResult UpdateStatus(int UserId, bool Status)
        {
            var user = _UserAccountService.GetById(UserId);

            if (user != null)
            {
                user.IsVerified = Status;
                _UserAccountService.Update(user);

                return Json(true, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
            }

            return Json(false, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
        
        [HttpGet]
        public JsonResult UpdateDeliveryBoy(int UserId, int DeliveryBoyId)
        {
            var user = _UserAccountService.GetById(UserId);

            if (user != null)
            {
                user.AssignedDeliveryBoyId = DeliveryBoyId;
                _UserAccountService.Update(user);

                return Json(true, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
            }

            return Json(false, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(UserAccount objModal)
        {
            try
            {
                if (objModal.UserId == 0)
                {
                    objModal.UserRoleId = 3;
                    var UserId = _UserAccountService.Create(objModal);
                }
                else
                {
                    var oldData = _UserAccountService.GetById(objModal.UserId);
                    if (oldData != null)
                    {
                        oldData.UserId = objModal.UserId;
                        oldData.UserUuid = objModal.UserUuid;
                        oldData.UserFirstName = objModal.UserFirstName;
                        oldData.UserLastName = objModal.UserLastName;
                        oldData.UserEmailAddress = objModal.UserEmailAddress;
                        oldData.UserPassword = objModal.UserPassword;

                        oldData.UserPhone = objModal.UserPhone;
                        oldData.IsPasswordReset = objModal.IsPasswordReset;
                        oldData.ActiveStatus = objModal.ActiveStatus;
                        oldData.Address = objModal.Address;
                        oldData.BillingAddress = objModal.BillingAddress;

                        oldData.Logitude = objModal.Logitude;
                        oldData.Latitude = objModal.Latitude;
                         


                        // oldData.UserProfilePicture= ;

                       

                    }
                    _UserAccountService.Update(oldData);
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
    }
}


 









