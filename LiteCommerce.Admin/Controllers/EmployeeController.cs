using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[Authorize(Roles =WebUserRoles.EMPLOYEEMANAGEMENT)]
    [Authorize]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Trang quản lý nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "",string searchCountry= "")
        {
            var model = new Models.EmployeePaginationResult()
            {
                Page = page,
                PageSize = AppSettings.DefaultPageSize,
                SearchValue = searchValue,
                searchCountry = searchCountry,
                RowCount = EmployeeBLL.Count_Employee(searchValue,searchCountry),
                Data = EmployeeBLL.Employee_List(page, AppSettings.DefaultPageSize, searchValue, searchCountry)
            };
            return View(model);
        }
        /// <summary>
        /// Trang thêm mới hoặc chỉnh sửa thông tin nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Input(String id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new employee";
                Employee newEmployee = new Employee();
                newEmployee.EmployeeID = 0;
                return View(newEmployee);
            }
            else
            {
                ViewBag.Title = "Edit employee";
                Employee editEmployee = EmployeeBLL.Get_Employee(Convert.ToInt32(id));
                if (editEmployee == null)
                    return RedirectToAction("Index");
                return View(editEmployee);
            }
        }
        [HttpPost]
        public ActionResult Input(Employee model, HttpPostedFileBase uploadPhoto)
        {

            //try
            //{
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(model.Address))
                model.Address = "";
            if (string.IsNullOrEmpty(model.City))
                model.City = "";
            if (string.IsNullOrEmpty(model.Country))
                model.Country = "";
            if (uploadPhoto != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(uploadPhoto.FileName));
                uploadPhoto.SaveAs(path);
                model.PhotoPath = "Images/" + Path.GetFileName(uploadPhoto.FileName);
            }
            //xử lý để đưa dữ liệu vào DB
            Employee data = EmployeeBLL.Check_Email(model.Email);
            if (model.EmployeeID == 0)//add
            {

                if (data != null)//check email
                {
                    ViewBag.Method = "Add";
                    ViewBag.Title = "Add new Employee";
                    ModelState.AddModelError("", "Email already exists!");
                    return View(model);
                }
                else
                {
                    int employeeID = EmployeeBLL.Add_Employee(model);
                    return RedirectToAction("Index");
                }
            }
            else //Edit
            {   if(data != null)
                {
                    if (data.EmployeeID != model.EmployeeID)
                    {
                        ViewBag.Method = "Edit";
                        ViewBag.Title = "Edit Employee";
                        ModelState.AddModelError("", "Email already exists!");
                        return View(model);
                    }
                    else
                    {
                        bool result = EmployeeBLL.Update_Employee(model);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    bool result = EmployeeBLL.Update_Employee(model);
                    return RedirectToAction("Index");
                }
                
            }
            //}
            //catch (Exception e)
            //{
            //    ModelState.AddModelError("", e.StackTrace);
            //    return View(model);
            //}
        }
        [HttpPost]
        public ActionResult Delete(string method = "", int[] employeeIDs = null)
        {
            try
            {
                if (employeeIDs != null)
                {
                    EmployeeBLL.Delete_Employee(employeeIDs);

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