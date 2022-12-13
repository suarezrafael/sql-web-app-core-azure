using sqlwebappcoreazure.Models;
using System.Data.SqlClient;

namespace sqlwebappcoreazure.Services
{
    public class ProductService
    {
        private static string db_source = "serverdbtestapp3000.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "@Hercules1989";
        private static string db_database = "testapp3000";

        private SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;
            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();

            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(product);
                }
            }
            conn.Close();
            return products;

        }
    }
}
