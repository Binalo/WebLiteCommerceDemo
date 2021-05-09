using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class SupplierDAL : ISupplierDAL
    {
        /// <summary>
        /// Chuỗi kết nối với database
        /// </summary>
        private string connectionString;
        /// <summary>
        /// Hàm dựng
        /// </summary>
        public SupplierDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Bổ sung một supplier và trả về ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add_Supplier(Supplier data)
        {
            int supplierId = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Suppliers
                                          (
	                                          CompanyName,
	                                          ContactName,
	                                          ContactTitle,
	                                          Address,
	                                          City,
	                                          Country,
	                                          Phone,
	                                          Fax,
	                                          HomePage
                                          )
                                          VALUES
                                          (
	                                          @CompanyName,
	                                          @ContactName,
	                                          @ContactTitle,
	                                          @Address,
	                                          @City,
	                                          @Country,
	                                          @Phone,
	                                          @Fax,
	                                          @HomePage
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);
                cmd.Parameters.AddWithValue("@HomePage", data.HomePage);

                supplierId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return supplierId;
        }
        /// <summary>
        /// Đếm số lượng supplier
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count_Supplier(string searchValue)
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
                                        from Suppliers
                                        where (@searchValue=N'')
                                               or(CompanyName like @searchValue)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }

            return rowCount;
        }
        /// <summary>
        /// Xóa nhiều supplier theo ID và trả về true or fals
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public bool Delete_Supplier(int[] supplierIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Suppliers
                                            WHERE   (SupplierID = @supplierId)
                                                 AND(SupplierID NOT IN(SELECT SupplierID FROM Products))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@supplierId", SqlDbType.Int);
                foreach (int supplierID in supplierIDs)
                {
                    cmd.Parameters["@supplierId"].Value = supplierID;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy thông tin của một supplier theo ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier Get_Supplier(int supplierID)
        {
            Supplier data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Suppliers WHERE SupplierID = @supplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CompanyName = Convert.ToString(dbReader["CompanyName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                            Fax = Convert.ToString(dbReader["Fax"]),
                            HomePage = Convert.ToString(dbReader["HomePage"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// Lấy danh sách supplier Phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Supplier> Supplier_List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
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
		                                        ROW_NUMBER() over(order by SupplierID) as RowNumber
                                        from Suppliers
                                        where (@searchValue=N'')
                                               or(CompanyName like @searchValue)
                                        ) as T
                                        where  (t.RowNumber between (@page*@pageSize)-@pageSize+1 and @page*@pageSize)";// chuỗi câu lệnh thực thi
                    cmd.CommandType = CommandType.Text; // kiểu câu lệnh procedu text 
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Supplier()
                            {
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CompanyName = Convert.ToString(dbReader["CompanyName"]),
                                ContactName = Convert.ToString(dbReader["ContactName"]),
                                ContactTitle = Convert.ToString(dbReader["ContactTitle"]),
                                Address = Convert.ToString(dbReader["Address"]),
                                City = Convert.ToString(dbReader["City"]),
                                Country = Convert.ToString(dbReader["Country"]),
                                Phone = Convert.ToString(dbReader["Phone"]),
                                Fax = Convert.ToString(dbReader["Fax"]),
                                HomePage = Convert.ToString(dbReader["HomePage"])
                            });
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// Chỉnh sửa thông tin Supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update_Supplier(Supplier data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Suppliers
                                    SET 
                                          CompanyName =  @CompanyName,
	                                      ContactName =  @ContactName,
	                                      ContactTitle = @ContactTitle,
	                                      Address =  @Address,
	                                      City=  @City,
	                                      Country = @Country,
	                                      Phone = @Phone,
	                                      Fax = @Fax,
	                                      HomePage = @HomePage
                                    WHERE SupplierID = @supplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CompanyName", data.CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", data.ContactTitle);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@Fax", data.Fax);
                cmd.Parameters.AddWithValue("@HomePage", data.HomePage);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
