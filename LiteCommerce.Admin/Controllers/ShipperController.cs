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
    public class ShipperController : Controller
    {
        /// <summary>
        /// Trang quản lý vận chuyển
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string searchValue = "")
        {
            var model = new Models.ShipperNoPagination()
            {
                RowCount = CatalogBLL.Count_Shipper(searchValue),
                SearchValue = searchValue,
                Data = CatalogBLL.Shipper_List(searchValue)
            };
            return View(model);
        }
        /// <summary>
        /// Trang thêm mới hoặc chỉnh sửa shipper
        /// </summary>
        /// <param name="id">Có truyển id là chỉnh sửa ngược lại là thêm mới</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            ViewBag.Title = "Edit supplier";
            Shipper editShipper = CatalogBLL.Get_Shipper(Convert.ToInt32(id));
            if (editShipper == null)
                return RedirectToAction("Index");
            return View(editShipper);
        }
        [HttpPost]
        public ActionResult Input(Shipper model)
        {
            try
            {
                    bool result = CatalogBLL.Update_Shipper(model);
                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }  
        }
        [HttpPost]
        public ActionResult Add(Shipper model)
        {
            try
            {
                    int result = CatalogBLL.Add_Shipper(model);
                    return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] shipperIDs = null)
        {
            try
            {
                if (shipperIDs != null)
                {
                    CatalogBLL.Delete_Shipper(shipperIDs);

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