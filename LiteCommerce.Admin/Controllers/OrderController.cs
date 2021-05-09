using LiteCommerce.Admin.Models;
using LiteCommerce.BusinessLayers;
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
    public class OrderController : Controller
    {
        /// <summary>
        /// Trang quản lý đơn đặt hàng
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = WebUserRoles.ORDERMANAGEMENT)]
        [Authorize]
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.OrderPaginationResult
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                SearchValue = searchValue,
                RowCount = OrderBLL.Count_Order(searchValue),
                Data = OrderBLL.Order_List(page, AppSettings.DefaultPageSize, searchValue),
                DetailData = OrderBLL.OrderDetail_List()
            };
            return View(model);
        }
        /// <summary>
        /// Trang tạo mới sản phẩm
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] orderIDs = null)
        {
            try
            {
                if (orderIDs != null)
                {
                    OrderBLL.Delete_Order(orderIDs);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
    }
}