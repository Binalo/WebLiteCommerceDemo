using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QuantityPerUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Descriptions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AttributeValues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}
