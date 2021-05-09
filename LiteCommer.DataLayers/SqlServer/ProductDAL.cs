using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductDAL : IProductDAL
    {
        /// <summary>
        /// 
        /// </summary>
        private string connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ProductDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add_Product(Product data)
        {
            int productID = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products
                                          (
                                              ProductName,
                                              SupplierID,
                                              CategoryID,
                                              QuantityPerUnit,
                                              UnitPrice,
                                              Descriptions,
                                              PhotoPath
                                          )
                                          VALUES
                                          (
	                                          @ProductName,
	                                          @SupplierID,
	                                          @CategoryID,
	                                          @QuantityPerUnit,
	                                          @UnitPrice,
	                                          @Descriptions,
	                                          @PhotoPath
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@QuantityPerUnit", data.QuantityPerUnit);
                cmd.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                cmd.Parameters.AddWithValue("@Descriptions", data.Descriptions);
                cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);

                productID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return productID;
        }
         /// <summary>
            /// 
            /// </summary>
            /// <param name="searchValue"></param>
            /// <param name="searchSupplier"></param>
            /// <param name="searchCategory"></param>
            /// <param name="searchPrice"></param>
            /// <returns></returns>
        public int Count_Product(string searchValue, string searchSupplier, string searchCategory, string searchPrice)
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
                    cmd.CommandText = @" select count(*)
                                        from View_Product
                                        where ((@searchValue=N'')
                                               or(ProductName like @searchValue)) 
                                               and((SupplierID like @searchSupplier) or(@searchSupplier=N''))
                                               and((CategoryID like @searchCategory) or(@searchCategory=N''))
                                               and((UnitPrice >= @searchPrice) or (@searchPrice=N''))
                                      ";// chuỗi câu lệnh thực thi
                    cmd.CommandType = CommandType.Text; // kiểu câu lệnh procedu text 
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@searchSupplier", searchSupplier);
                    cmd.Parameters.AddWithValue("@searchCategory", searchCategory);
                    cmd.Parameters.AddWithValue("@searchPrice", searchPrice);
                    rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                }
                connection.Close();
            }
            return rowCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductIDs"></param>
        /// <returns></returns>
        public bool Delete_Product(int[] productIDs)
        {
            bool result = true;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Products
                                           WHERE    (ProductID = @ProductID)
                                                 AND(ProductID NOT IN(SELECT ProductID FROM OrderDetails))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                foreach (int productID in productIDs)
                {
                    cmd.Parameters["@ProductID"].Value = productID;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public Product Get_Product(int productID)
        {
            Product data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM View_Product WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", productID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierName = Convert.ToString(dbReader["CompanyName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            UnitPrice = Convert.ToDouble(dbReader["UnitPrice"]),
                            Descriptions = Convert.ToString(dbReader["Descriptions"]),
                            QuantityPerUnit = Convert.ToString(dbReader["QuantityPerUnit"]),
                            PhotoPath = Convert.ToString(dbReader["PhotoPath"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SelectList> List_Category()
        {
            List<SelectList> data = new List<SelectList>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select CategoryID,CategoryName
                                        from Categories";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new SelectList
                            {
                                Value = Convert.ToInt32(dbReader["CategoryID"]),
                                Text = Convert.ToString(dbReader["CategoryName"])
                            });
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SelectList> List_Supplier()
        {
            List<SelectList> data = new List<SelectList>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"select SupplierID,CompanyName
                                        from Suppliers";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new SelectList
                            {
                                Value = Convert.ToInt32(dbReader["SupplierID"]),
                                Text = Convert.ToString(dbReader["CompanyName"])
                            });
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="searchSupplier"></param>
        /// <param name="searchCategory"></param>
        /// <param name="searchPrice"></param>
        /// <returns></returns>
        public List<Product> Product_List(int page, int pageSize, string searchValue, string searchSupplier, string searchCategory, string searchPrice)
        {
            List<Product> data = new List<Product>();
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
		                                        ROW_NUMBER() over(order by ProductID) as RowNumber
                                        from View_Product
                                        where ((@searchValue=N'')
                                               or(ProductName like @searchValue)) 
                                               and((SupplierID like @searchSupplier) or(@searchSupplier=N''))
                                               and((CategoryID like @searchCategory) or(@searchCategory=N''))
                                               and((UnitPrice >=@searchPrice) or (@searchPrice=N''))
                                        ) as T
                                        where t.RowNumber between (@page*@pageSize)-@pageSize+1 and @page*@pageSize";// chuỗi câu lệnh thực thi
                    cmd.CommandType = CommandType.Text; // kiểu câu lệnh procedu text 
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    cmd.Parameters.AddWithValue("@searchSupplier", searchSupplier);
                    cmd.Parameters.AddWithValue("@searchCategory", searchCategory);
                    cmd.Parameters.AddWithValue("@searchPrice", searchPrice);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Product()
                            {
                                ProductID = Convert.ToInt32(dbReader["ProductID"]),
                                ProductName = Convert.ToString(dbReader["ProductName"]),
                                SupplierName = Convert.ToString(dbReader["CompanyName"]),
                                SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                                CategoryName= Convert.ToString(dbReader["CategoryName"]),
                                CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                                UnitPrice = Convert.ToDouble(dbReader["UnitPrice"]),
                                Descriptions = Convert.ToString(dbReader["Descriptions"]),
                                QuantityPerUnit = Convert.ToString(dbReader["QuantityPerUnit"]),
                                PhotoPath = Convert.ToString(dbReader["PhotoPath"])
                            });
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update_Product(Product data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET 
                                          ProductName =  @ProductName,
	                                      SupplierID =  @SupplierID,
	                                      CategoryID = @CategoryID,
	                                      QuantityPerUnit =  @QuantityPerUnit,
	                                      UnitPrice=  @UnitPrice,
	                                      Descriptions = @Descriptions,
	                                      PhotoPath = @PhotoPath
                                    WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@QuantityPerUnit", data.QuantityPerUnit);
                cmd.Parameters.AddWithValue("@UnitPrice", data.UnitPrice);
                cmd.Parameters.AddWithValue("@Descriptions", data.Descriptions);
                cmd.Parameters.AddWithValue("@PhotoPath", data.PhotoPath);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }
            return rowsAffected > 0;
        }
    }
}
