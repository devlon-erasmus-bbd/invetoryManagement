using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public class DatabaseConnection
{
    private SqlConnection ConnectToDatabase()
    {
        string connectionString = @"Data Source=(local);Initial Catalog=invetory_manager;Integrated Security=true";
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();

        return conn;
    }

    private void CloseConnectionToDatabase(SqlConnection conn)
    {
        conn.Close();
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
                //AcquiredDate = Convert.ToDateTime(dataReader.GetValue(14).ToString(), culture),
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

    public void AddItem(ItemModel item)
    {

    }

    public void AddCompany(CompanyModel company)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Company] (company_name, company_description, created_date)" +
            "VALUES ('" + company.CompanyName + "', '" + company.CompanyDescription + "', GETDATE()); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public void AddSupplier(SupplierModel supplier)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Supplier] (supplier_name, supplier_contact_number, created_date)" +
            "VALUES ('" + supplier.SupplierName + "', '" + supplier.SupplierContactNumber + "', GETDATE()); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public void AddItemCategory(ItemCategoryModel itemCategory)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[ItemCategory] (item_category_name, item_category_description)" +
            "VALUES ('" + itemCategory.ItemCategoryName + "', '" + itemCategory.ItemCategoryDescription + "'); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }
}