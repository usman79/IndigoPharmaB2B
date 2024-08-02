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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace IndigoAdmin.Controllers
{
    public class BrandController : Controller
    {
        IBrandService _BrandService;
        IInventoryService _InventoryService;
        private readonly IHostingEnvironment hostingEnvironment;
        public BrandController(IBrandService BrandService, IInventoryService InventoryService, IHostingEnvironment environment)
        {
            _BrandService = BrandService;
            _InventoryService = InventoryService;
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllBrands()
        {
            var listCompanies = _BrandService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(Brand objModal)
        {
            try
            {
                if (objModal.BrandId == 0)

                {
                    objModal.CreatedAt = DateTime.Now;
                    var BrandId = _BrandService.Create(objModal);
                    if (objModal.BrandLogo != null)
                    {
                        var uniqueFileName = GetUniqueFileName(objModal.BrandLogo.FileName);
                        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "brands", BrandId + "");
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        var filePath = Path.Combine(uploads, uniqueFileName);
                        objModal.BrandLogo.CopyTo(new FileStream(filePath, FileMode.Create));
                        var oldData = _BrandService.GetById(BrandId);
                        oldData.Image = uniqueFileName;
                        _BrandService.Update(oldData);
                        //to do : Save uniqueFileName  to your db table   
                    }
                }
                else
                {
                    var oldData = _BrandService.GetById(objModal.BrandId);
                    if (oldData != null)
                    {
                        oldData.Title = objModal.Title;
                        oldData.Summary = objModal.Summary;
                         
                        oldData.BrandId = objModal.BrandId;
                      // oldData.Image= ;

                        oldData.ModifiedAt = DateTime.Now;

                        if (objModal.BrandLogo != null)
                        {
                            var filePath = "";
                            var uniqueFileName = GetUniqueFileName(objModal.BrandLogo.FileName);
                            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "brands", objModal.BrandId + "");
                            if (oldData.Image != null && oldData.Image != "")
                            {
                                filePath = Path.Combine(uploads, uniqueFileName);
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }
                            }
                            if (!Directory.Exists(uploads))
                            {
                                Directory.CreateDirectory(uploads);
                            }
                            filePath = Path.Combine(uploads, uniqueFileName);
                            objModal.BrandLogo.CopyTo(new FileStream(filePath, FileMode.Create));
                            oldData.Image = uniqueFileName;
                        }

                    }
                    _BrandService.Update(oldData);
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
                _BrandService.Delete(Id);
            }
            catch (Exception e)
            {
                string strErr = e.Message;
                return Unauthorized();
            }
            return Ok();
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}




