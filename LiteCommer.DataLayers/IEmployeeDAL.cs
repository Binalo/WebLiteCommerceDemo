using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Thêm một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add_Employee(Employee data);
        /// <summary>
        /// Chỉnh sửa một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update_Employee(Employee data);
        /// <summary>
        /// Xóa nhiều nhân viên theo ID
        /// </summary>
        /// <param name="EmployeeIDs"></param>
        /// <returns></returns>
        bool Delete_Employee(int[] EmployeeIDs);
        /// <summary>
        /// Lấy danh sách nhân viên có phân trang và tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> Employee_List(int page, int pageSize, string searchValue, string searchCountry);
        /// <summary>
        /// Lấy ra một nhân viên theo ID
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        Employee Get_Employee(int employeeID);
        int Count_Employee(string searchValue,string searchCountry);
        Employee Check_Email(string email);
    }
}
