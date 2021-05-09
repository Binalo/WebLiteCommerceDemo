using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize] //Kiểm tra cookie yêu cầu đăng nhập để thực hiện chức năng
    public class AccountController : Controller
    {
        /// <summary>
        /// Trang quản lý thông tin user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            WebUserData userData = User.GetUserData();
            Employee editEmployee = EmployeeBLL.Get_Employee(Convert.ToInt32(userData.UserID));
            return View(editEmployee);

        }
        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string id, string newPWd, string currentPWd)
        {
            bool result = false;
            Employee employee = EmployeeBLL.Get_Employee(Convert.ToInt32(id));
            if (employee.Password == MD5.Encrypt(currentPWd))
            {
                result = UserAccountBLL.PWd_Update(id, MD5.Encrypt(newPWd));
            }
            else
            {
                result = false;
            }
            return Json(new
            {
                status = result
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult Signout()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// Trang đăng nhập và kiểm tra đăng nhập
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string email, string pwd)
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                var userAccount = UserAccountBLL.Authorize(email, MD5.Encrypt(pwd), UserAccountTypes.Employee);

                if (userAccount != null)
                {
                    WebUserData cookieData = new WebUserData()
                    {
                        UserID = userAccount.UserID,
                        FullName = userAccount.FullName,
                        GroupName = userAccount.GroupName,
                        LoginTime = DateTime.Now,
                        SessionID = Session.SessionID,
                        ClientIP = Request.UserHostAddress,
                        Photo = userAccount.Photo,
                        Title = userAccount.Title
                    };
                    FormsAuthentication.SetAuthCookie(cookieData.ToCookieString(), false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Login information not right");//lưu lại thông báo lỗi vào ""
                    ViewBag.Email = email;
                    return View();
                }

            }

        }
        public ActionResult Input(Employee model, HttpPostedFileBase uploadPhoto)
        {
            try
            {
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

                if (data != null)
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
            catch (Exception e)
            {
                ModelState.AddModelError("", e.StackTrace);
                return View(model);
            }
        }
        /// <summary>
        /// Quên mật khẩu không bắt đăng nhập
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPwd(string email)
        {
            return View();
        }
    }
}