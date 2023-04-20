using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    private SqlConnection ConnectToDatabase()
    {
        // string connectionString = @"Data Source=(local);Initial Catalog=invetory_manager;Integrated Security=true";
        string connectionString = @"Data Source=.\DEVLON_LOCAL;Initial Catalog=invetory_manager;Trusted_Connection=True;Integrated Security=True";

        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        return conn;
    }

    private void CloseConnectionToDatabase(SqlConnection conn)
    {
        conn.Close();
    }

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

    public List<StaffModel> GetListOfStaffs()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT s.staff_id, s.staff_name " +
            "FROM [invetory_manager].[dbo].[Staff] as s";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<StaffModel> staffModels = new List<StaffModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            StaffModel staff = new StaffModel()
            {
                StaffId = (int)dataReader.GetValue(0),
                StaffName = dataReader.GetValue(1).ToString(),
            };

            staffModels.Add(staff);
        }

        CloseConnectionToDatabase(conn);

        return staffModels;
    }

    public List<CompanyModel> GetListOfCompany() {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT c.company_id, c.company_name, c.company_description, c.created_date " +
            "FROM [invetory_manager].[dbo].[Company] as c";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<CompanyModel> companyModels = new List<CompanyModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            CompanyModel company = new CompanyModel()
            {
                CompanyId = (int)dataReader.GetValue(0),
                CompanyName = dataReader.GetValue(1).ToString(),
                CompanyDescription = dataReader.GetValue(2).ToString(),
                CreatedDate = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture)
            };

            companyModels.Add(company);
        }

        CloseConnectionToDatabase(conn);

        return companyModels;
    }

    public List<SupplierModel> GetListOfSupplier()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT s.supplier_id, s.supplier_name, s.supplier_contact_number, s.created_date " +
            "FROM [invetory_manager].[dbo].[Supplier] as s";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<SupplierModel> supplierModels = new List<SupplierModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            SupplierModel supplier = new SupplierModel()
            {
                SupplierId = (int)dataReader.GetValue(0),
                SupplierName = dataReader.GetValue(1).ToString(),
                SupplierContactNumber = dataReader.GetValue(2).ToString(),
                DateAdded = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture)
            };

            supplierModels.Add(supplier);
        }

        CloseConnectionToDatabase(conn);

        return supplierModels;
    }
    
    private CompanyModel GetCompanyByCompanyId(int company_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT c.company_id, c.company_name, c.company_description, c.created_date " +
            "FROM [invetory_manager].[dbo].[Company] as c " +
            "WHERE c.company_id = " + company_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        CompanyModel company = new CompanyModel();

        while (dataReader.Read())
        {
            company.CompanyId = (int)dataReader.GetValue(0);
            company.CompanyName = dataReader.GetValue(1).ToString();
            company.CompanyDescription = dataReader.GetValue(2).ToString();
            company.CreatedDate = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture);
        }

        CloseConnectionToDatabase(conn);
        return company;
    }

    private SupplierModel GetSupplierBySupplierId(int supplier_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT s.supplier_id, s.supplier_name, s.supplier_contact_number, s.created_date " +
            "FROM [invetory_manager].[dbo].[Supplier] as s " +
            "WHERE s.supplier_id = " + supplier_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        SupplierModel supplier = new SupplierModel();

        while (dataReader.Read())
        {
            supplier.SupplierId = (int)dataReader.GetValue(0);
            supplier.SupplierName = dataReader.GetValue(1).ToString();
            supplier.SupplierContactNumber = dataReader.GetValue(2).ToString();
            supplier.DateAdded = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture);
        }

        CloseConnectionToDatabase(conn);
        return supplier;
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

    public void AddStaff(StaffModel staff)
    {
        if (staff.StaffName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Staff] (staff_name)" +
            "VALUES ('" + staff.StaffName + "'); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
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

    public void AddCompany(CompanyModel company)
    {
        if (company.CompanyName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Company] (company_name, company_description, created_date)" +
            "VALUES ('" + company.CompanyName + "', '" + company.CompanyDescription + "', GETDATE()); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public void AddSupplier(SupplierModel supplier)
    {
        if (supplier.SupplierName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Supplier] (supplier_name, supplier_contact_number, created_date)" +
            "VALUES ('" + supplier.SupplierName + "', '" + supplier.SupplierContactNumber + "', GETDATE()); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public void EditStaff(StaffModel staff)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "UPDATE [invetory_manager].[dbo].[Staff] " +
            "SET staff_name = '" + staff.StaffName + "' " +
            "WHERE staff_id = " + staff.StaffId;
        SqlCommand command = new SqlCommand(sql, conn);
        command.ExecuteNonQuery();

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
}