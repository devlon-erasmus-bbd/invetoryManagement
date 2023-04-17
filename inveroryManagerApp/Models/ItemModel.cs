namespace inveroryManagerApp.Models;
public class ItemCategoryModel
{
    public int ItemId { get; set; }
    public int CompanyId { get; set; }
    public int SupplierId { get; set; }
    public int ItemCategory { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public datetime AcquiredDate { get; set; }
    public double CostPrice { get; set; }
    public double SellPrice { get; set; }
    public int Quantity { get; set; }
    public datetime ExpiryDate { get; set; }
}