﻿using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class EmployeePaginationResult : PaginationResult
    {
        public List<Employee> Data;
        public string searchCountry;
    }
}