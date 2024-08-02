using IndigoAdmin.DAL.Data.EF;
using IndigoAdmin.Models;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace IndigoAdmin.Controllers
{
    public class ProductController : Controller
    {
        IProductService _PrdocutService;
        IBrandService _BrandService;
        private readonly IHostingEnvironment hostingEnvironment;

        public ProductController(IProductService PrdocutService, IBrandService BrandService, IHostingEnvironment environment)
        {
            _PrdocutService = PrdocutService;
            _BrandService = BrandService;
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
           var brands= _BrandService.GetAll();
            ViewBag.Brands = brands;
            return View();
        }

        [HttpGet]
        public JsonResult GetAllPrdocuts()
        {
            var listCompanies = _PrdocutService.GetAll();

            return Json(listCompanies, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }

        [HttpPost]
        public ActionResult Save(Product objModal)
        {
            try
            {
                if (objModal.ProductId == 0)
                {
                    objModal.CreatedAt = DateTime.Now;
                    var ProductId = _PrdocutService.Create(objModal);
                    if (objModal.MedLogo != null)
                    {
                        var uniqueFileName = GetUniqueFileName(objModal.MedLogo.FileName);
                        var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads", ProductId+"");
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        var filePath = Path.Combine(uploads, uniqueFileName);
                        objModal.MedLogo.CopyTo(new FileStream(filePath, FileMode.Create));
                        var oldData = _PrdocutService.GetById(ProductId);
                        oldData.Image = uniqueFileName;
                        _PrdocutService.Update(oldData);
                        //to do : Save uniqueFileName  to your db table   
                    }
                }
                else
                {
                    var oldData = _PrdocutService.GetById(objModal.ProductId);
                    if (oldData != null)
                    {
                        oldData.Title = objModal.Title;
                        oldData.Summary = objModal.Summary;
                        oldData.BarCode = objModal.BarCode;
                        oldData.BrandId = objModal.BrandId;
                        oldData.BatchNumber = objModal.BatchNumber;
                        oldData.Quantity = objModal.Quantity;
                        oldData.Price = objModal.Price;
                        oldData.Discount = objModal.Discount;
                        oldData.MaxPerOrder = objModal.MaxPerOrder;
                        oldData.MinWarningLimit = objModal.MinWarningLimit;
                        oldData.ModifiedAt = DateTime.Now;
                        if (objModal.MedLogo != null)
                        {
                            var filePath = "";
                            var uniqueFileName = GetUniqueFileName(objModal.MedLogo.FileName);
                            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads", objModal.ProductId + "");
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
                            objModal.MedLogo.CopyTo(new FileStream(filePath, FileMode.Create));
                            oldData.Image = uniqueFileName; 
                        }
                    }
                    _PrdocutService.Update(oldData);
                }
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

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                _PrdocutService.Delete(Id);
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
