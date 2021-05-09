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
    public class UserAccountBLL
    {
        private static IAccountDAL userAccountDB { get; set; }
        public static void Initialize(string connectionString)
        {
            userAccountDB = new EmployeeUserAccountDAL(connectionString);
        }
        public static UserAccount Authorize(string userName, string password,UserAccountTypes userType)
        {
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    break;
                //case UserAccountTypes.Customer:
                //    userAccountDB = new EmployeeUserAccountDAL(connectionString);
                //    break;
                //case UserAccountTypes.Shipper:
                //    userAccountDB = new EmployeeUserAccountDAL(connectionString);
                //    break;
                default:
                    throw new Exception("User type is not valid");
            }
            return userAccountDB.Authorize(userName, password);
        }
        public static bool PWd_Update(string id,string newPWd)
        {
            return userAccountDB.PWd_Update(id, newPWd);
        }
    }
}
