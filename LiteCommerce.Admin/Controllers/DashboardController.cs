using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Trang xem dashboard chung 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int year = 2020)
        {
            var model = new Models.DashBoardModel
            {
                OrderDashBoard = DashBoardBLL.OrderStatisticsByYear(year),
                RevenueDashBoard = DashBoardBLL.RevenueStatisticsByYear(year)
            };
            return View(model);
        }
        [HttpGet]
        public JsonResult Input(int year)
        {
            Dashboard orderDashboard = DashBoardBLL.RevenueStatisticsByYear(year);
            return Json(new 
            {
               result = orderDashboard
            }, JsonRequestBehavior.AllowGet);
        }
    }
}