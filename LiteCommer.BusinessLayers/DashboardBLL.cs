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
    public static class DashBoardBLL
    {
        private static IDashboardDAL DashBoardDB { get; set; }
        public static void Initialize(string connectionString)
        {
            DashBoardDB = new DashboardDAL(connectionString);
        }
        public static Dashboard OrderStatisticsByYear(int year)
        {
            return DashBoardDB.OrderStatisticsByYear(year);
        }
        public static Dashboard RevenueStatisticsByYear(int year)
        {
            return DashBoardDB.RevenueStatisticsByYear(year);
        }
    }
}
