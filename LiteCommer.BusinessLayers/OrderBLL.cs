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
    public static class OrderBLL
    {
        /// <summary>
        /// 
        /// </summary>
        private static IOrderDAL OrderDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            OrderDB = new OrderDAL(connectionString);
        }
        /// <summary>
        /// trả về danh sách đơn hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Order> Order_List(int page, int pageSize, string searchValue)
        {
            return OrderDB.Order_List(page, pageSize, searchValue);
        }
        /// <summary>
        /// trả về số lượng đơn hàng
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count_Order(string searchValue)
        {
            return OrderDB.Count_Order(searchValue);
        }
        /// <summary>
        /// trả về danh sách chi tiết đơn hàng
        /// </summary>
        /// <returns></returns>
        public static List<OrderDetail> OrderDetail_List()
        {
            return OrderDB.OrderDetail_List();
        }
        /// <summary>
        /// Xóa nhiều đơn hàng 
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public static bool Delete_Order(int[] orderIDs)
        {
            return OrderDB.Delete_Order(orderIDs);
        }
    }
}
