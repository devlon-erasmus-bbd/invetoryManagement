namespace inveroryManagerApp.Models;
public class ItemModel
{
    public int ItemId { get; set; }
    public CompanyModel? Company { get; set; }
    public SupplierModel? Supplier { get; set; }
    public ItemCategoryModel? ItemCategory { get; set; }
    public string? ItemName { get; set; }
    public string? ItemDescription { get; set; }
    public DateTime AcquiredDate { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellPrice { get; set; }
    public int Quantity { get; set; }
    public DateTime? ExpiryDate { get; set; }
}