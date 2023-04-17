namespace inveroryManagerApp.Models;
public class ItemModel
{
    public int ItemId { get; set; }
    public int CompanyId { get; set; }
    public int SupplierId { get; set; }
    public int ItemCategory { get; set; }
    public string? ItemName { get; set; }
    public string? ItemDescription { get; set; }
    public DateTime AcquiredDate { get; set; }
    public double CostPrice { get; set; }
    public double SellPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpiryDate { get; set; }
}