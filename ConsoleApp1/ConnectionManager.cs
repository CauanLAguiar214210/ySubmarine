using System;
using System.Data.SqlClient;
namespace ConsoleApp1
{
    internal class ConnectionManager
    {
        private static string ConnStr = @"Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = ySubmarine; Data Source = CAPPUCCINO";

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnStr);
        }

        public static void TestarConexao()
        {
            try
            {
                var connection = ConnectionManager.GetSqlConnection();
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
