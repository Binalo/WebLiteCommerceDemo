using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class PaginationResult
    {
         public int Page { get; set; }
         public int PageSize { get; set; }
         public int RowCount { get; set; }
         public string SearchValue { get; set; }
         public int PageCount
        {
            get
            {
                int pageCount = 1;
                pageCount = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    pageCount += 1;
                return pageCount;
            }
        }
    }
}