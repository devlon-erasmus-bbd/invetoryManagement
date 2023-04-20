using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
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
}