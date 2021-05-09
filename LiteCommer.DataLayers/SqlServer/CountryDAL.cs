using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CountryDAL : ICountryDAL
    {
        private string connectionString;
        /// <summary>
        /// Hàm dựng
        /// </summary>
        public CountryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> List_Country()
        {
            List<string> data = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Country FROM Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(Convert.ToString(dbReader["Country"]));
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
