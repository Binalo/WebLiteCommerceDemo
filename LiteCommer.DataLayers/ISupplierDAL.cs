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
    public interface ISupplierDAL
    {
        /// <summary>
        /// Bổ sung thêm 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add_Supplier(Supplier data);
        /// <summary>
        /// Chỉnh sửa thông tin supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update_Supplier(Supplier data);
        /// <summary>
        /// Xóa nhiều supplier theo ID
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        bool Delete_Supplier(int[] supplierIDs);
        /// <summary>
        /// Lấy supplier theo ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Supplier Get_Supplier(int supplierID);
        /// <summary>
        /// Lấy danh sách supplier có phân trang và tìm kiếm theo searchValue
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Supplier> Supplier_List(int page, int pageSize, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count_Supplier(string searchValue);
    }
}
