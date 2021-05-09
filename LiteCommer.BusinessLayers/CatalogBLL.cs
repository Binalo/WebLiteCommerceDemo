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
    /// <summary>
    /// 
    /// </summary>
    public static class CatalogBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private static ISupplierDAL SupplierDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private static ICustomerDAL CustomerDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private static IShipperDAL ShipperDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private static ICategoryDAL CategoryDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private static IProductDAL ProductDB { get; set; }
        private static ICountryDAL CountryDB { get; set; }
        /// <summary>
        /// Hàm này để khởi tạo chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new SupplierDAL(connectionString);
            CustomerDB = new CustomerDAL(connectionString);
            ShipperDB = new ShipperDAL(connectionString);
            CategoryDB = new CategoryDAL(connectionString);
            ProductDB = new ProductDAL(connectionString);
            CountryDB = new CountryDAL(connectionString);
        }
        /// <summary>
        /// Lấy danh sách supplier có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> Supplier_List(int page, int pageSize, string searchValue)
        {
            return SupplierDB.Supplier_List(page, pageSize, searchValue);
        }

        /// <summary>
        /// Lấy danh sách Customer có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> Customer_List(int page, int pageSize, string searchValue)
        {
            return CustomerDB.Customer_List(page, pageSize, searchValue);
        }
        /// <summary>
        /// Lấy danh sách shipper theo searchValue
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> Shipper_List(string searchValue)
        {
            return ShipperDB.Shipper_List(searchValue);
        }
        /// <summary>
        /// Lấy danh sách tên các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<string> List_Country()
        {
            return CountryDB.List_Country();
        }
        public static List<SelectList> SelectList_Supplier()
        {
            return ProductDB.List_Supplier();
        }
        public static List<SelectList> SelectList_Category()
        {
            return ProductDB.List_Category();
        }
        /// <summary>
        /// Lấy ra một supplier theo ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier Get_Supplier(int supplierID)
        {
            return SupplierDB.Get_Supplier(supplierID);
        }
        /// <summary>
        /// Lấy ra một khách hàng theo ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer Get_Customer(string customerID)
        {
            return CustomerDB.Get_Customer(customerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper Get_Shipper(int shipperID)
        {
            return ShipperDB.Get_Shipper(shipperID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category Get_Category(int categoryID)
        {
            return CategoryDB.Get_Category(categoryID);
        }
        public static Product Get_Product(int productID)
        {
            return ProductDB.Get_Product(productID);
        }
        /// <summary>
        /// Lấy danh sách category theo searchValue
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> Category_List(string searchValue)
        {
            return CategoryDB.Category_List(searchValue);
        }
        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="searchSupplier"></param>
        /// <param name="searchCategory"></param>
        /// <param name="searchPrice"></param>
        /// <returns></returns>
        public static List<Product> Product_List(int page, int pageSize, string searchValue, string searchSupplier, string searchCategory, string searchPrice)
        {
            return ProductDB.Product_List(page, pageSize, searchValue, searchSupplier, searchCategory, searchPrice);
        }
        public static int Count_Product(string searchValue, string searchSupplier, string searchCategory, string searchPrice)
        {
            return ProductDB.Count_Product(searchValue, searchSupplier, searchCategory, searchPrice);
        }
        /// <summary>
        /// Đếm số lượng supplier
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count_Supplier(string searchValue)
        {
            return SupplierDB.Count_Supplier(searchValue);
        }
        /// <summary>
        /// Đếm số lượng loại được lấy ra
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count_Category(string searchValue)
        {
            return CategoryDB.Count_Catogory(searchValue);
        }
        /// <summary>
        /// Đếm số lượng khách hàng được lấy ra
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count_Customer(string searchValue)
        {
            return CustomerDB.Count_Customer(searchValue);
        }
        /// <summary>
        /// Đếm số lượng shipper được lấy ra
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count_Shipper(string searchValue)
        {
            return ShipperDB.Count_Shipper(searchValue);
        }
        /// <summary>
        /// Bổ sung thêm supplier 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add_Supplier(Supplier data)
        {
            return SupplierDB.Add_Supplier(data);
        }
        /// <summary>
        /// Thêm 1 shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add_Shipper(Shipper data)
        {
            return ShipperDB.Add_Shipper(data);
        }
        /// <summary>
        /// Thêm một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add_Customer(Customer data)
        {
            return CustomerDB.Add_Customer(data);
        }
        /// <summary>
        /// Thêm một loại sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add_Category(Category data)
        {
            return CategoryDB.Add_Category(data);
        }
        public static int Add_Product(Product data)
        {
            return ProductDB.Add_Product(data);
        }
        /// <summary>
        /// Chỉnh sửa thông tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update_Supplier(Supplier data)
        {
            return SupplierDB.Update_Supplier(data);
        }
        /// <summary>
        /// Chỉnh sửa thông tin shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update_Shipper(Shipper data)
        {
            return ShipperDB.Update_Shipper(data);
        }
        /// <summary>
        /// Chỉnh sửa thông tin khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update_Customer(Customer data)
        {
            return CustomerDB.Update_Customer(data);
        }
        /// <summary>
        /// Chỉnh sửa loại
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update_Category(Category data)
        {
            return CategoryDB.Update_Category(data);
        }
        public static bool Update_Product(Product data)
        {
            return ProductDB.Update_Product(data);
        }
        /// <summary>
        /// Delete nhiều supplier
        /// </summary>
        /// <param name="SupplierIDs"></param>
        /// <returns></returns>
        public static bool Delete_Supplier(int[] supplierIDs)
        {
            return SupplierDB.Delete_Supplier(supplierIDs);
        }
        /// <summary>
        /// Xóa nhiều nhà vận chuyển
        /// </summary>
        /// <param name="shipperIDs"></param>
        /// <returns></returns>
        public static bool Delete_Shipper(int[] shipperIDs)
        {
            return ShipperDB.Delete_Shipper(shipperIDs);
        }
        /// <summary>
        /// Xóa nhiều khách hàng
        /// </summary>
        /// <param name="customerIDs"></param>
        /// <returns></returns>
        public static bool Delete_Customer(string[] customerIDs)
        {
            return CustomerDB.Delete_Customer(customerIDs);
        }
        /// <summary>
        /// Xóa nhiều loại sản phẩm theo danh sách id
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        public static bool Delete_Category(int[] categoryIDs)
        {
            return CategoryDB.Delete_Category(categoryIDs);
        }
        /// <summary>
        /// Xóa nhiều sản phẩm theo id
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        public static bool Delete_Products(int[] productIDs)
        {
            return ProductDB.Delete_Product(productIDs);
        }
    }
}
