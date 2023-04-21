using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    public List<CustomerModel> GetListOfCustomers()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT cus.customer_id, cus.customer_name, cus.customer_contact_number " +
            "FROM [invetory_manager].[dbo].[Customer] as cus";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<CustomerModel> customerModels = new List<CustomerModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            CustomerModel customer = new CustomerModel()
            {
                CustomerId = (int)dataReader.GetValue(0),
                CustomerName = dataReader.GetValue(1).ToString(),
                CustomerContactNumber = dataReader.GetValue(2).ToString(),
            };

            customerModels.Add(customer);
        }

        CloseConnectionToDatabase(conn);

        return customerModels;
    }

    public void AddCustomer(CustomerModel customer)
    {
        if (customer.CustomerName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Customer] (customer_name, customer_contact_number)" +
            "VALUES ('" + customer.CustomerName + "', '" + customer.CustomerContactNumber + "'); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public void EditCustomer(CustomerModel customer)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "UPDATE [invetory_manager].[dbo].[Customer] " +
            "SET customer_name = '" + customer.CustomerName + "', customer_contact_number = '" + customer.CustomerContactNumber + "' " +
            "WHERE customer_id = " + customer.CustomerId;
        SqlCommand command = new SqlCommand(sql, conn);
        command.ExecuteNonQuery();

        CloseConnectionToDatabase(conn);
    }

    public CustomerModel GetCustomerByCustomerId(int customer_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT cus.customer_id, cus.customer_name, cus.customer_contact_number " +
            "FROM [invetory_manager].[dbo].[Customer] as cus " +
            "WHERE cus.customer_id = " + customer_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        CustomerModel customer = new CustomerModel();

        while (dataReader.Read())
        {
            customer.CustomerId = (int)dataReader.GetValue(0);
            customer.CustomerName = dataReader.GetValue(1).ToString();
            customer.CustomerContactNumber = dataReader.GetValue(2).ToString();
        }

        CloseConnectionToDatabase(conn);
        return customer;
    }
}
