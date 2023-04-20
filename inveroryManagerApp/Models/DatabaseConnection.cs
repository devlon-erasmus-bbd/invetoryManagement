using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public class DatabaseConnection
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

    public List<ItemModel> GetListOfItems()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT c.company_id, c.company_name, c.company_description, c.created_date, " +
            "s.supplier_id, s.supplier_name, s.supplier_contact_number, s.created_date, " +
            "ic.item_category_id, ic.item_category_name, ic.item_category_description, " +
            "i.item_id, i.item_name, i.item_description, i.acquired_date, i.cost_price, i.sell_price, i.quantity, i.expiry_date " +
            "FROM [invetory_manager].[dbo].[Item] as i " +
            "INNER JOIN [invetory_manager].[dbo].[Company] as c " +
            "ON i.fk_company_id = c.company_id " +
            "INNER JOIN [invetory_manager].[dbo].[Supplier] as s " +
            "ON i.fk_supplier_id = s.supplier_id " +
            "INNER JOIN [invetory_manager].[dbo].[ItemCategory] as ic " +
            "ON i.fk_item_category_id = ic.item_category_id";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<ItemModel> itemModels = new List<ItemModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            CompanyModel company = new CompanyModel() { 
                CompanyId = (int)dataReader.GetValue(0),
                CompanyName = dataReader.GetValue(1).ToString(), 
                CompanyDescription = dataReader.GetValue(2).ToString(),
                CreatedDate = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture)
            };
            SupplierModel supplier = new SupplierModel()
            {
                SupplierId = (int)dataReader.GetValue(4),
                SupplierName = dataReader.GetValue(5).ToString(),
                SupplierContactNumber = dataReader.GetValue(6).ToString(),
                DateAdded = Convert.ToDateTime(dataReader.GetValue(7).ToString(), culture)
            };
            ItemCategoryModel itemCategory = new ItemCategoryModel()
            {
                ItemCategoryId = (int)dataReader.GetValue(8),
                ItemCategoryName = dataReader.GetValue(9).ToString(),
                ItemCategoryDescription = dataReader.GetValue(10).ToString()
            };

            ItemModel itemModel = new ItemModel()
            {
                ItemId = (int)dataReader.GetValue(11),
                Company = company,
                Supplier = supplier,
                ItemCategory = itemCategory,
                ItemName = dataReader.GetValue(12).ToString(),
                ItemDescription = dataReader.GetValue(13).ToString(),
                AcquiredDate = Convert.ToDateTime(dataReader.GetValue(14).ToString(), culture),
                CostPrice = (decimal)dataReader.GetValue(15),
                SellPrice = (decimal)dataReader.GetValue(16),
                Quantity = (int)dataReader.GetValue(17),
            };

            if (dataReader.GetValue(18).ToString() != "")
                itemModel.ExpiryDate = Convert.ToDateTime(dataReader.GetValue(18).ToString(), culture);
            else
                itemModel.ExpiryDate = null;

            itemModels.Add(itemModel);
        }

        CloseConnectionToDatabase(conn);

        return itemModels;
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

    public List<ItemCategoryModel> GetItemCategoryModels()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT ic.item_category_id, ic.item_category_name, ic.item_category_description " +
            "FROM [invetory_manager].[dbo].[ItemCategory] as ic";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<ItemCategoryModel> itemCategoryModels = new List<ItemCategoryModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            ItemCategoryModel itemCategory = new ItemCategoryModel()
            {
                ItemCategoryId = (int)dataReader.GetValue(0),
                ItemCategoryName = dataReader.GetValue(1).ToString(),
                ItemCategoryDescription = dataReader.GetValue(2).ToString()
            };

            itemCategoryModels.Add(itemCategory);
        }

        CloseConnectionToDatabase(conn);

        return itemCategoryModels;
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

    private ItemCategoryModel GetItemCategoryByItemCategoryId(int item_category_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT ic.item_category_id, ic.item_category_name, ic.item_category_description " +
            "FROM [invetory_manager].[dbo].[ItemCategory] as ic " +
            "WHERE ic.item_category_id = " + item_category_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        ItemCategoryModel itemCategory = new ItemCategoryModel();

        while (dataReader.Read())
        {
            itemCategory.ItemCategoryId = (int)dataReader.GetValue(0);
            itemCategory.ItemCategoryName = dataReader.GetValue(1).ToString();
            itemCategory.ItemCategoryDescription = dataReader.GetValue(2).ToString();
        }

        CloseConnectionToDatabase(conn);
        return itemCategory;
    }

    public ItemModel GetItemByItemId(int item_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT c.company_id, c.company_name, c.company_description, c.created_date, " +
            "s.supplier_id, s.supplier_name, s.supplier_contact_number, s.created_date, " +
            "ic.item_category_id, ic.item_category_name, ic.item_category_description, " +
            "i.item_id, i.item_name, i.item_description, i.acquired_date, i.cost_price, i.sell_price, i.quantity, i.expiry_date " +
            "FROM [invetory_manager].[dbo].[Item] as i " +
            "INNER JOIN [invetory_manager].[dbo].[Company] as c " +
            "ON i.fk_company_id = c.company_id " +
            "INNER JOIN [invetory_manager].[dbo].[Supplier] as s " +
            "ON i.fk_supplier_id = s.supplier_id " +
            "INNER JOIN [invetory_manager].[dbo].[ItemCategory] as ic " +
            "ON i.fk_item_category_id = ic.item_category_id " +
            "WHERE i.item_id = " + item_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        ItemModel itemModel = new ItemModel();

        while (dataReader.Read())
        {
            CompanyModel company = new CompanyModel()
            {
                CompanyId = (int)dataReader.GetValue(0),
                CompanyName = dataReader.GetValue(1).ToString(),
                CompanyDescription = dataReader.GetValue(2).ToString(),
                CreatedDate = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture)
            };
            SupplierModel supplier = new SupplierModel()
            {
                SupplierId = (int)dataReader.GetValue(4),
                SupplierName = dataReader.GetValue(5).ToString(),
                SupplierContactNumber = dataReader.GetValue(6).ToString(),
                DateAdded = Convert.ToDateTime(dataReader.GetValue(7).ToString(), culture)
            };
            ItemCategoryModel itemCategory = new ItemCategoryModel()
            {
                ItemCategoryId = (int)dataReader.GetValue(8),
                ItemCategoryName = dataReader.GetValue(9).ToString(),
                ItemCategoryDescription = dataReader.GetValue(10).ToString()
            };

            itemModel.ItemId = (int)dataReader.GetValue(11);
            itemModel.Company = company;
            itemModel.Supplier = supplier;
            itemModel.ItemCategory = itemCategory;
            itemModel.ItemName = dataReader.GetValue(12).ToString();
            itemModel.ItemDescription = dataReader.GetValue(13).ToString();
            itemModel.AcquiredDate = Convert.ToDateTime(dataReader.GetValue(14).ToString(), culture);
            itemModel.CostPrice = (decimal)dataReader.GetValue(15);
            itemModel.SellPrice = (decimal)dataReader.GetValue(16);
            itemModel.Quantity = (int)dataReader.GetValue(17);

            if (dataReader.GetValue(18).ToString() != "")
                itemModel.ExpiryDate = Convert.ToDateTime(dataReader.GetValue(18).ToString(), culture);
            else
                itemModel.ExpiryDate = null;

            itemModel.listCompany = company.CompanyId;
            itemModel.listSupplier = supplier.SupplierId;
            itemModel.listItemCategory = itemCategory.ItemCategoryId;
        }

        CloseConnectionToDatabase(conn);

        return itemModel;
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

    public void AddItem(ItemModel item)
    {
        if (item.ItemName == null || item.listCompany == 0 || item.listSupplier == 0 || item.listItemCategory == 0)
            return;

        SqlConnection conn = ConnectToDatabase();

        var exp = (item.ExpiryDate?.ToString("'yyyy-MM-dd'") ?? "NULL");

        String sql = "INSERT INTO [invetory_manager].[dbo].[Item] " +
            "(fk_company_id, fk_supplier_id, fk_item_category_id, item_name, item_description, " +
            "acquired_date, cost_price, sell_price, quantity, expiry_date) " +
            "VALUES (" + item.listCompany + ", " + item.listSupplier + ", " + item.listItemCategory + ", '" + item.ItemName + "', '" + item.ItemDescription + "', " +
            "'" + item.AcquiredDate.ToString("yyyy-MM-dd") + "', " + item.CostPrice + ", " + item.SellPrice + ", " + item.Quantity + ", " + (item.ExpiryDate?.ToString("'yyyy-MM-dd'") ?? "NULL") + "); " +
            "SELECT SCOPE_IDENTITY()";
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

    public void AddItemCategory(ItemCategoryModel itemCategory)
    {
        if (itemCategory.ItemCategoryName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[ItemCategory] (item_category_name, item_category_description)" +
            "VALUES ('" + itemCategory.ItemCategoryName + "', '" + itemCategory.ItemCategoryDescription + "'); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public List<OrderModel> GetOrders()
    {
        SqlConnection conn = ConnectToDatabase();
        // String sql = "SELECT o.order_id, o.fk_item_id, o.quantity, o.discount, o.price_paid " +
        //     "FROM [invetory_manager].[dbo].[Order] as o";
        String sql = "SELECT i.item_id, i.item_name, o.order_id, o.quantity, o.discount, o.price_paid " +
            "FROM [invetory_manager].[dbo].[Orders] as o " +
            "INNER JOIN [invetory_manager].[dbo].[Item] as i " +
            "ON o.fk_item_id = i.item_id ";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<OrderModel> orderModels = new List<OrderModel>();

        while (dataReader.Read())
        {
            ItemModel item = new ItemModel() {
                ItemId = (int)dataReader.GetValue(0),
                ItemName = dataReader.GetValue(1).ToString(),
            };

            OrderModel order = new OrderModel()
            {
                OrderId = (int)dataReader.GetValue(2),
                Item = item,
                Quantity = (int)dataReader.GetValue(3),
                Discount = (decimal)dataReader.GetValue(4),
                PricePaid = (decimal)dataReader.GetValue(5),
            };

            orderModels.Add(order);
        }
        CloseConnectionToDatabase(conn);

        return orderModels;
    }

    public void AddOrder(OrderModel order)
    {
        if (order.listItem == 0 || order.Quantity == 0 || order.Discount == 0 || order.PricePaid == 0)
            return;

        SqlConnection conn = ConnectToDatabase();
        String sql = "INSERT INTO [invetory_manager].[dbo].[Orders] ([fk_item_id], [quantity], [discount], [price_paid]) " +
            "VALUES(" + order.listItem + " ," + order.Quantity + " ," + order.Discount + " ," + order.PricePaid + ")";
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

    public void EditItem(ItemModel item)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "UPDATE [invetory_manager].[dbo].[Item] " +
            "SET fk_company_id = " + item.listCompany + ", fk_supplier_id = " + item.listSupplier + ", fk_item_category_id = " + item.listItemCategory + ", " +
            "item_name = '" + item.ItemName + "', item_description = '" + item.ItemDescription + "', acquired_date = '" + item.AcquiredDate.ToString("yyyy-MM-dd") + "', " +
            "cost_price = " + item.CostPrice + ", sell_price = " + item.SellPrice + ", quantity = " + item.Quantity + ", " +
            "expiry_date = " + (item.ExpiryDate?.ToString("'yyyy-MM-dd'") ?? "NULL") +  " " + 
            "WHERE item_id = " + item.ItemId;
        SqlCommand command = new SqlCommand(sql, conn);
        command.ExecuteNonQuery();

        CloseConnectionToDatabase(conn);
    }
}