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
    public class SupplierController : Controller
    {
        /// <summary>
        /// Trang quản hiển thị: danh sách supplier, "liên kết các chức năng liên quan"
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1 ,string searchValue= "")
        {
          
            var model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                SearchValue = searchValue,
                RowCount = CatalogBLL.Count_Supplier(searchValue),
                Data = CatalogBLL.Supplier_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Add new supplier";
                    Supplier newSupplier = new Supplier();
                    newSupplier.SupplierID = 0;
                    return View(newSupplier);
                }
                else
                {
                    ViewBag.Title = "Edit supplier";
                    Supplier editSupplier = CatalogBLL.Get_Supplier(Convert.ToInt32(id));
                    if (editSupplier == null)
                        return RedirectToAction("Index");
                    return View(editSupplier);
                } 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Supplier model)
        {
            try 
            {
                //Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(model.ContactTitle))
                    model.ContactTitle = "";
                if (string.IsNullOrEmpty(model.ContactName))
                    model.ContactName = "";
                if (string.IsNullOrEmpty(model.Address))
                    model.Address = "";
                if (string.IsNullOrEmpty(model.City))
                    model.City = "";
                if (string.IsNullOrEmpty(model.Country))
                    model.Country = "";
                if (string.IsNullOrEmpty(model.Phone))
                    model.Phone = "";
                if (string.IsNullOrEmpty(model.Fax))
                    model.Fax = "";
                if (string.IsNullOrEmpty(model.HomePage))
                    model.HomePage = "";
                //xử lý để đưa dữ liệu vào DB
                if (model.SupplierID == 0)
                {
                    int supplierID = CatalogBLL.Add_Supplier(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool result = CatalogBLL.Update_Supplier(model);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(model);
            } 
        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] supplierIDs = null)
        {
            try 
            {
                if (supplierIDs != null)
                {
                    CatalogBLL.Delete_Supplier(supplierIDs);

                }
                return RedirectToAction("Index");
            } catch
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}