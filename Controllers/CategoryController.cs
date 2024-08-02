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
    public class CategoryController : Controller
    {
        ICategoryService _CategoryService;
        IInventoryService _InventoryService;

        public CategoryController(ICategoryService CategoryService, IInventoryService InventoryService)
        {
            _CategoryService = CategoryService;
            _InventoryService = InventoryService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            var listCompanies = _CategoryService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(Category objModal)
        {
            try
            {
                if (objModal.CategoryId == 0)
                {
                    objModal.CreatedAt = DateTime.Now;
                    var CategoryId = _CategoryService.Create(objModal);
                }
                else
                {
                    var oldData = _CategoryService.GetById(objModal.CategoryId);
                    if (oldData != null)
                    {
                        oldData.Title = objModal.Title;
                        oldData.Summary = objModal.Summary;

                        oldData.CategoryId = objModal.CategoryId;
                        // oldData.Image= ;

                        oldData.ModifiedAt = DateTime.Now;

                    }
                    _CategoryService.Update(oldData);
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
                _CategoryService.Delete(Id);
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








