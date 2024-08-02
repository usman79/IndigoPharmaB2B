using IndigoAdmin.Models;
using IndigoAdmin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IndigoAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _PrdocutService;
        private readonly IUserAccountService _UserAccountService;
        private readonly IOrderService _OrderService;

        public HomeController(ILogger<HomeController> logger, IProductService PrdocutService, IUserAccountService UserAccountService, IOrderService OrderService)
        {
            _logger = logger;
            _PrdocutService = PrdocutService;
            _UserAccountService = UserAccountService;
            _OrderService = OrderService;
        }

        public IActionResult Index()
        {
            var test = Environment.Version;
            var allData = _PrdocutService.GetAll();
            ViewBag.HappyCustomer = _UserAccountService.GetCount();
            ViewBag.ProductCount = _PrdocutService.GetCount();
            ViewBag.OrderCount = _OrderService.GetCount();
            ViewBag.MonthWiseOrders = _OrderService.GetMonthWiseCount();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
