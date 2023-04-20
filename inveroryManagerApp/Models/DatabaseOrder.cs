using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    public List<OrderModel> GetOrders()
    {
        SqlConnection conn = ConnectToDatabase();
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

    public OrderModel GetOrderById(int order_id)
    {
        SqlConnection conn = ConnectToDatabase();
        String sql = "SELECT i.item_id, i.item_name, o.order_id, o.quantity, o.discount, o.price_paid " +
            "FROM [invetory_manager].[dbo].[Orders] as o " +
            "INNER JOIN [invetory_manager].[dbo].[Item] as i " +
            "ON o.fk_item_id = i.item_id " +
            "WHERE o.order_id = " + order_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        OrderModel order = new OrderModel();

        while (dataReader.Read())
        {
            ItemModel item = new ItemModel() {
                ItemId = (int)dataReader.GetValue(0),
                ItemName = dataReader.GetValue(1).ToString(),
            };

            order.OrderId = (int)dataReader.GetValue(2);
            order.Item = item;
            order.Quantity = (int)dataReader.GetValue(3);
            order.Discount = (decimal)dataReader.GetValue(4);
            order.PricePaid = (decimal)dataReader.GetValue(5);
        }

        CloseConnectionToDatabase(conn);

        return order;
    }
}