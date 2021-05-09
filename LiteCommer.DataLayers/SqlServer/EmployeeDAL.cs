using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class EmployeeDAL : IEmployeeDAL
    {   /// <summary>
        /// 
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public Employee Check_Email(string email)
        {
            Employee data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE Email = @Email";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Email", email);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            HomePhone = Convert.ToString(dbReader["HomePhone"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            HireDate = Convert.ToDateTime(dbReader["HireDate"]),
                            PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
                            Title = Convert.ToString(dbReader["Title"]),
                            Password = Convert.ToString(dbReader["Password"])
                        };
                    }
                }
                connection.Close();
            }
            return data;

        }
        /// <summary>
        /// Bổ sung một employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add_Employee(Employee data)
        {
            int employeeID = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees
                                          (
                                              LastName,
                                              FirstName,
                                              BirthDate,
                                              Email,
                                              Notes,
                                              PhotoPath,
                                              Password
                                          )
                                          VALUES
                                          (
                                              @LastName,
                                              @FirstName,
                                              @BirthDate,
                                              @Email,
                                              @Notes,
                                              @PhotoPath,
                                              @Password
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@PhotoPath",data.PhotoPath);
                cmd.Parameters.AddWithValue("@Password", "e10adc3949ba59abbe56e057f20f883e");

                employeeID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return employeeID;
        }
        /// <summary>
        /// Đếm số lượng nhân viên
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count_Employee(string searchValue,string searchCountry)
        {
            int rowCount = 0;
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select count(*)
                                        from Employees
                                        where ((@searchValue=N'')
                                               or(FirstName like @searchValue) or (LastName like @searchValue))
                                               AND ((Country like @searchCountry) or(@searchCountry=N''))";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@searchCountry", searchCountry);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }

            return rowCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeIDs"></param>
        /// <returns></returns>
        public bool Delete_Employee(int[] employeeIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Employees
                                            WHERE (EmployeeID = @employeeID)
                                                   AND (EmployeeID NOT IN (SELECT EmployeeID FROM Orders) ) ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@employeeID", SqlDbType.Int);
                foreach (int employeeID in employeeIDs)
                {
                    cmd.Parameters["@employeeID"].Value = employeeID;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy danh sách nhân viên có phân trang và tìm kiếm
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số dòng </param>
        /// <param name="searchValue">giá trị tìm kiếm</param>
        /// <returns></returns>
        public List<Employee> Employee_List(int page, int pageSize, string searchValue,string searchCountry)
        {
            List<Employee> data = new List<Employee>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select *
                                        from
                                        (
                                        select *,
		                                        ROW_NUMBER() over(order by EmployeeID) as RowNumber
                                        from Employees
                                        where     ((@searchValue=N'')
                                               OR (FirstName like @searchValue) or (LastName like @searchValue))
                                               AND ((Country like @searchCountry) or (@searchCountry=N''))
                                        ) as T
                                        where t.RowNumber between (@page*@pageSize)-@pageSize+1 and @page*@pageSize";// chuỗi câu lệnh thực thi
                    cmd.CommandType = CommandType.Text; // kiểu câu lệnh procedu text 
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@searchCountry", searchCountry);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Employee()
                            {
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                FirstName = Convert.ToString(dbReader["FirstName"]),
                                LastName = Convert.ToString(dbReader["LastName"]),
                                BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                                /*Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                HomePhone = Convert.ToString(dbReader["HomePhone"]),*/
                                Notes = Convert.ToString(dbReader["Notes"]),
                                Email = Convert.ToString(dbReader["Email"]),
/*                                HireDate = Convert.ToDateTime(dbReader["HireDate"]),
*/                                PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
/*                                Title = Convert.ToString(dbReader["Title"]),
*/                                Password = Convert.ToString(dbReader["Password"])
                            });
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// Lấy ra 1 nhân viên theo ID
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public Employee Get_Employee(int employeeID)
        {
            Employee data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE EmployeeID = @employeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            HomePhone = Convert.ToString(dbReader["HomePhone"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            HireDate = Convert.ToDateTime(dbReader["HireDate"]),
                            PhotoPath = Convert.ToString(dbReader["PhotoPath"]),
                            Title = Convert.ToString(dbReader["Title"]),
                            Password = Convert.ToString(dbReader["Password"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update_Employee(Employee data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET 
                                            LastName = @LastName,
                                            FirstName = @FirstName,
                                            Title = @Title,
                                            BirthDate = @BirthDate,
                                            HireDate = @HireDate,
                                            Email  = @Email,
                                            Address = @Address,
                                            City = @City,
                                            Country = @Country,
                                            HomePhone = @HomePhone,
                                            Notes = @Notes,
                                            PhotoPath = @PhotoPath
                                    WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@Title", data.Title);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@HireDate", data.HireDate);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);
                cmd.Parameters.AddWithValue("@HomePhone", data.HomePhone);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected > 0; ;
        }
    }
}
