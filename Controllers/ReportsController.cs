using IndigoAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;
        private readonly IOrderService _OrderService;

        public ReportsController(ILogger<ReportsController> logger, IOrderService OrderService)
        {
            _logger = logger;
            _OrderService = OrderService;
        }
        public IActionResult Index()
        {
            ViewBag.MonthWiseOrders = _OrderService.GetMonthWiseCount();
            return View();
        }

        [HttpGet]
        public JsonResult DateWiseOrders( DateTime StartDate, DateTime EndDate)
        {
            var listData = _OrderService.GetDateWise( StartDate, EndDate);

            return Json(listData, new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver() });
        }
    }
}
