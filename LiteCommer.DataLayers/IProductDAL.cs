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
    public interface IProductDAL
    {
        /// <summary>
        /// Thêm một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add_Product(Product data);
        /// <summary>
        /// Chỉnh sửa một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update_Product(Product data);
        /// <summary>
        /// Xóa nhiều nhân viên theo ID
        /// </summary>
        /// <param name="ProductIDs"></param>
        /// <returns></returns>
        bool Delete_Product(int[] ProductIDs);
        /// <summary>
        /// Lấy danh sách nhân viên có phân trang và tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Product> Product_List(int page, int pageSize, string searchValue,string searchSupplier,string searchCategory,string searchPrice);
        /// <summary>
        /// Lấy ra một nhân viên theo ID
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Product Get_Product(int ProductID);
        int Count_Product(string searchValue, string searchSupplier, string searchCategory, string searchPrice);
        /// <summary>
        /// Lấy danh sách tên các supplier
        /// </summary>
        /// <returns></returns>
        List<SelectList> List_Supplier();
        /// <summary>
        /// Lấy danh sách tên các category
        /// </summary>
        /// <returns></returns>
        List<SelectList> List_Category();
    }
}
