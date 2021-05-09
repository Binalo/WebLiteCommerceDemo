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
    public class CustomerController : Controller
    {
       /// <summary>
       /// Trang quản lý khách hàng
       /// </summary>
       /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                SearchValue = searchValue,
                RowCount = CatalogBLL.Count_Customer(searchValue),
                Data = CatalogBLL.Customer_List(page, AppSettings.DefaultPageSize, searchValue)
            };
            return View(model);
        }
        /// <summary>
        /// Tạo mới hoặc chỉnh sửa thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Customer";
                Customer newCustomer = new Customer();
                newCustomer.CustomerID = null;
                ViewBag.Method = "Add";
                return View(newCustomer);
            }
            else
            {
                ViewBag.Title = "Edit Customer";
                Customer editCustomer = CatalogBLL.Get_Customer(id);
                if (editCustomer == null)
                    return RedirectToAction("Index");
                ViewBag.Method = "Edit";
                return View(editCustomer);
            }
        }
        [HttpPost]
        public ActionResult Input(Customer model,string method)
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
                if (method=="Add" )
                {

                    if (CatalogBLL.Get_Customer(model.CustomerID) != null)
                    {
                        ViewBag.Method = "Add";
                        ViewBag.Title = "Add new Customer";
                        ModelState.AddModelError("", "ID already exists!");
                        return View(model);
                    }
                    else
                    {
                        int resultAddCustomer = CatalogBLL.Add_Customer(model);
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    bool resultUpdateCustomer = CatalogBLL.Update_Customer(model);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.StackTrace);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Delete(string method = "", string[] customerIDs = null)
        {
            try
            {
                if (customerIDs != null)
                {
                    CatalogBLL.Delete_Customer(customerIDs);
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