using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class EmployeeBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private static IEmployeeDAL EmployeeDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            EmployeeDB = new EmployeeDAL(connectionString);
        }
        /// <summary>
        /// Lấy ra một nhân viên theo id
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee Get_Employee(int employeeID)
        {
            return EmployeeDB.Get_Employee(employeeID);
        }
        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add_Employee(Employee data)
        {
            return EmployeeDB.Add_Employee(data);
        }
        /// <summary>
        /// Chỉnh sửa thông tin nhân viên 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update_Employee(Employee data)
        {
            return EmployeeDB.Update_Employee(data);
        }
        /// <summary>
        /// Đếm số lượng nhân viên được lấy ra
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count_Employee(string searchValue,string searchCountry)
        {
            return EmployeeDB.Count_Employee(searchValue, searchCountry);
        }
        /// <summary>
        /// Lấy ra danh sách nhân viên có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> Employee_List(int page, int pageSize, string searchValue,string searchCountry)
        {
            return EmployeeDB.Employee_List(page, pageSize, searchValue,searchCountry);
        }
        public static bool Delete_Employee(int[] employeeIDs)
        {
            return EmployeeDB.Delete_Employee(employeeIDs);
        }
        public static Employee Check_Email(string email)
        {
            return EmployeeDB.Check_Email(email);
        }
    }
}
