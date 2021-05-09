using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class DashboardDAL : IDashboardDAL
    {
        private string connectionString;
        public DashboardDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Dashboard OrderStatisticsByYear(int year)
        {
            Dashboard data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Year, COALESCE([1] , 0) AS January, COALESCE([2] , 0) AS February, COALESCE([3] , 0) AS March, COALESCE([4] , 0) AS April, COALESCE([5] , 0) AS May, COALESCE([6] , 0) AS June, COALESCE([7] , 0) AS July, COALESCE([8] , 0) AS August, COALESCE([9] , 0) AS September, COALESCE([10], 0) AS October, COALESCE([11], 0) AS November, COALESCE([12], 0) AS December 
                                    FROM   
                                    (SELECT YEAR(OrderDate) as Year  , MONTH(OrderDate) AS Month   , HD.OrderID    FROM Orders HD	WHERE YEAR(OrderDate) = @Year)  TEMP PIVOT(  COUNT(OrderID)    FOR Month IN ([1], [2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS PivotTable";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Year", year);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Dashboard()
                        {
                            Year = Convert.ToInt32(dbReader["Year"]),
                            January = Convert.ToInt32(dbReader["January"]),
                            February = Convert.ToInt32(dbReader["February"]),
                            March = Convert.ToInt32(dbReader["March"]),
                            April = Convert.ToInt32(dbReader["April"]),
                            May = Convert.ToInt32(dbReader["May"]),
                            June = Convert.ToInt32(dbReader["June"]),
                            July = Convert.ToInt32(dbReader["July"]),
                            August = Convert.ToInt32(dbReader["August"]),
                            September = Convert.ToInt32(dbReader["September"]),
                            October = Convert.ToInt32(dbReader["October"]),
                            November = Convert.ToInt32(dbReader["November"]),
                            December = Convert.ToInt32(dbReader["December"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
        public Dashboard RevenueStatisticsByYear(int year)
        {
            Dashboard data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Year, COALESCE([1] , 0) AS January, COALESCE([2] , 0) AS February, COALESCE([3] , 0) AS March, COALESCE([4] , 0) AS April, COALESCE([5] , 0) AS May, COALESCE([6] , 0) AS June, COALESCE([7] , 0) AS July, COALESCE([8] , 0) AS August, COALESCE([9] , 0) AS September, COALESCE([10], 0) AS October, COALESCE([11], 0) AS November, COALESCE([12], 0) AS December 
                                    FROM 
                                    (SELECT YEAR(OrderDate) as Year  , MONTH(OrderDate) AS Month ,(CT.Quantity*CT.UnitPrice)-(CT.Quantity*CT.UnitPrice)*CT.Discount AS ToTal FROM Orders HD JOIN OrderDetails CT ON CT.OrderID = HD.OrderID WHERE YEAR(HD.OrderDate) = @Year)  TEMP PIVOT( SUM(ToTal) FOR Month IN ([1], [2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS PivotTable";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Year", year);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Dashboard()
                        {
                            Year = Convert.ToInt32(dbReader["Year"]),
                            January = Convert.ToInt32(dbReader["January"]),
                            February = Convert.ToInt32(dbReader["February"]),
                            March = Convert.ToInt32(dbReader["March"]),
                            April = Convert.ToInt32(dbReader["April"]),
                            May = Convert.ToInt32(dbReader["May"]),
                            June = Convert.ToInt32(dbReader["June"]),
                            July = Convert.ToInt32(dbReader["July"]),
                            August = Convert.ToInt32(dbReader["August"]),
                            September = Convert.ToInt32(dbReader["September"]),
                            October = Convert.ToInt32(dbReader["October"]),
                            November = Convert.ToInt32(dbReader["November"]),
                            December = Convert.ToInt32(dbReader["December"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
