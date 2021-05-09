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
    public interface IShipperDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add_Shipper(Shipper data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update_Shipper(Shipper data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperIDs"></param>
        /// <returns></returns>
        bool Delete_Shipper(int[] ShipperIDs);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        Shipper Get_Shipper(int ShipperID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> Shipper_List(string searchValue);
        int Count_Shipper(string searchValue);
    }
}
