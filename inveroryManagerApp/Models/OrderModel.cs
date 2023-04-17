namespace inveroryManagerApp.Models;
public class OrderModel
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int BillId { get; set; }
    public int Quantity { get; set; }
    public double Discount { get; set; }
    public double PricePaid { get; set; }
}