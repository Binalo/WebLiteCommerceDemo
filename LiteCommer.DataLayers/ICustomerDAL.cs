using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Thêm một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add_Customer(Customer data);
        /// <summary>
        /// Chỉnh sửa một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update_Customer(Customer data);
        /// <summary>
        /// Xóa khách hàng theo ID
        /// </summary>
        /// <param name="CustomerIDs"></param>
        /// <returns></returns>
        bool Delete_Customer(string[] CustomerIDs);
        /// <summary>
        /// Lấy danh sách khách hàng có phân trang và tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Customer> Customer_List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Lấy ra một khách hàng theo ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        Customer Get_Customer(string CustomerID);
        int Count_Customer(string searchValue);
    }
}
