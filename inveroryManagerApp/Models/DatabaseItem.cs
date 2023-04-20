using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
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