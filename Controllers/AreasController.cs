using IndigoAdmin.DAL.Data.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
using IndigoAdmin.Helpers;
using IndigoAdmin.Models;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
 
 
using System.Data;
 
 
using System.Net;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace IndigoAdmin.Controllers
{
    public class AreasController : Controller
    {
        IndigoDbContext db = new IndigoDbContext();
        // GET: AreasController
        public ActionResult Index()
        {
            var areas= db.Areas.OrderBy(x => x.Title).ToList();
            return View(areas);
        }

        // GET: AreasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AreasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(include: "Title")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(area);
        }

        // GET: AreasController/Edit/5
        public ActionResult Edit(int id)
        {

            return View(db.Areas.FirstOrDefault(x=>x.AreaId==id));
        }

        // POST: AreasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(include: "AreaId,Title")] Area area)
        {
            if (ModelState.IsValid)
            {
                 
                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(area);
        }

        // GET: AreasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AreasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
