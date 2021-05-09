using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ProductController : Controller
    {
        /// <summary>
        /// Trang quản lý sản phẩm
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "", string searchSupplier = "", string searchCategory = "", string searchPrice = "")
        {
            var model = new Models.ProductPaginationResult
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                SearchValue = searchValue,
                SearchSupplier = searchSupplier,
                SearchCategory = searchCategory,
                SearchPrice = searchPrice,
                RowCount = CatalogBLL.Count_Product(searchValue, searchSupplier, searchCategory, searchPrice),
                DataProduct = CatalogBLL.Product_List(page, AppSettings.DefaultPageSize, searchValue, searchSupplier, searchCategory, searchPrice)
            };
            return View(model);
        }
        /// <summary>
        /// Trang tạo mới chỉnh sứa sản phẩm
        /// </summary>
        /// <param name="id">Nếu có truyền id thì chỉnh sửa ngược lại là thêm mới</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new product";
                Product newProduct = new Product();
                newProduct.ProductID = 0;
                return View(newProduct);
            }
            else
            {
                ViewBag.Title = "Edit product";
                Product editProduct = CatalogBLL.Get_Product(Convert.ToInt32(id));
                if (editProduct == null)
                    return RedirectToAction("Index");
                return View(editProduct);
            }
        }
        [HttpPost]
        public ActionResult Input(Product model, HttpPostedFileBase uploadPhoto)
        {
            try
            {
                //Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(model.Descriptions))
                    model.Descriptions = "";
                if (string.IsNullOrEmpty(model.PhotoPath))
                    model.PhotoPath = "";
                if (uploadPhoto != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(uploadPhoto.FileName));
                    uploadPhoto.SaveAs(path);
                    model.PhotoPath = "Images/" + Path.GetFileName(uploadPhoto.FileName);
                }
                //xử lý để đưa dữ liệu vào DB
                if (model.ProductID == 0)
                {
                    int productID = CatalogBLL.Add_Product(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool result = CatalogBLL.Update_Product(model);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] productIDs = null)
        {
            try
            {
                if (productIDs != null)
                {
                    CatalogBLL.Delete_Products(productIDs);

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