using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// Khởi tạo các chức năng tác nghiệp
    /// </summary>
    public static class BusinessLayerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;
            CatalogBLL.Initialize(connectionString);
            EmployeeBLL.Initialize(connectionString);
            UserAccountBLL.Initialize(connectionString);
            OrderBLL.Initialize(connectionString);
            DashBoardBLL.Initialize(connectionString);
        }
    }
}