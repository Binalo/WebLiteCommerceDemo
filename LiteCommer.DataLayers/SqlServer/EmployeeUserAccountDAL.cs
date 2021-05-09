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
    /// <summary>
    /// Kiểm tra thông tin đăng nhập của nhân viên
    /// </summary>
    public class EmployeeUserAccountDAL : IAccountDAL
    {
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionstring"></param>
        public EmployeeUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public UserAccount Authorize(string email, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select EmployeeID,FirstName,LastName,PhotoPath,GroupName,Title
                                        from Employees
                                        where (Email=@email) and  (Password=@pWd)
                                        ";// chuỗi câu lệnh thực thi
                    cmd.CommandType = CommandType.Text; // kiểu câu lệnh procedu text 
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pWd", password);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data = new UserAccount()
                            {
                                UserID = Convert.ToString(dbReader["EmployeeID"]),
                                FullName = Convert.ToString(dbReader["FirstName"]) + " " + Convert.ToString(dbReader["LastName"]),
                                Photo = Convert.ToString(dbReader["PhotoPath"]),
                                GroupName = Convert.ToString(dbReader["GroupName"]),
                                Title = Convert.ToString(dbReader["Title"])
                            };
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        public bool PWd_Update(string id, string newPWd)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    cmd.CommandText = @"UPDATE Employees
                                        SET 
                                               Password = @Password
                                        WHERE  EmployeeID = @EmployeeID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    cmd.Parameters.AddWithValue("@Password", newPWd);
                    rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                    connection.Close();
                }
            }
            return rowsAffected>0;
        }
    }
}
