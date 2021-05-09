using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ShipperNoPagination
    {
       public  List<Shipper> Data;
       public  int RowCount;
       public string SearchValue;
    }
}