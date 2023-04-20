using System.ComponentModel.DataAnnotations;

namespace inveroryManagerApp.Models;
public class OrderModel
{
    public int OrderId { get; set; }
    public ItemModel? Item { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public decimal PricePaid { get; set; }

    [Display(Name = "Item")]
    public int listItem { get; set; }
}