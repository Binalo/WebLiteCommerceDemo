using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IDashboardDAL
    {
        Dashboard OrderStatisticsByYear(int year);
        Dashboard RevenueStatisticsByYear(int year);
    }
}
