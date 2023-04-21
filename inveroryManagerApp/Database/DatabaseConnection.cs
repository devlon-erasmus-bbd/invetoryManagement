using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    private SqlConnection ConnectToDatabase()
    {
        //string connectionString = @"Server=KaydenK\SQLEXPRESS;Database=invetory_manager;Trusted_Connection=True;MultipleActiveResultSets=true";
        //string connectionString = @"Data Source=.\DEVLON_LOCAL;Initial Catalog=invetory_manager;Trusted_Connection=True;Integrated Security=True";
        string connectionString = @"Server=(local);Database=invetory_manager;Trusted_Connection=True;MultipleActiveResultSets=true";

        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        return conn;
    }

    private void CloseConnectionToDatabase(SqlConnection conn)
    {
        conn.Close();
    }

}