using System.ComponentModel.DataAnnotations;

namespace inveroryManagerApp.Models;
public class ItemModel
{
    public int ItemId { get; set; }

    public CompanyModel? Company { get; set; }

    public SupplierModel? Supplier { get; set; }

    public ItemCategoryModel? ItemCategory { get; set; }

    [Required(ErrorMessage = "Item Name is required. It cannot be empty.")]
    public string? ItemName { get; set; }

    public string? ItemDescription { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy/mm/dd}", ApplyFormatInEditMode = true)]
    public DateTime AcquiredDate { get; set; }


    //[DataType(DataType.Currency)]
    //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
    public decimal CostPrice { get; set; }


    public decimal SellPrice { get; set; }

    public int Quantity { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy/mm/dd}", ApplyFormatInEditMode = true)]
    public DateTime? ExpiryDate { get; set; }

    [Display(Name = "Companys")]
    public int listCompany { get; set; }

    [Display(Name = "Suppliers")]
    public int listSupplier { get; set; }

    [Display(Name = "ItemCategorys")]
    public int listItemCategory { get; set; }


}