using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inveroryManagerApp.Models;
public class ItemModel
{
    public int ItemId { get; set; }

    public CompanyModel? Company { get; set; }

    public SupplierModel? Supplier { get; set; }

    public ItemCategoryModel? ItemCategory { get; set; }

    public string? ItemName { get; set; }

    public string? ItemDescription { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{yyyy/mm/dd}", ApplyFormatInEditMode = true)]
    public DateTime AcquiredDate { get; set; }


    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:c2}", ApplyFormatInEditMode = true)]
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



    /*
    public IEnumerable<SelectListItem>? GetCompanyData() {
        DatabaseConnection db = new DatabaseConnection();
        List<CompanyModel> comModel = db.GetListOfCompany();
        List<SelectListItem> companies = new List<SelectListItem>();

        foreach (CompanyModel comModelItem in comModel)
        {
            companies.Add(new SelectListItem { Value = comModelItem.CompanyId.ToString(), Text = comModelItem.CompanyName });
        }
        return companies;

    }
    public IEnumerable<SelectListItem> SelectedComapny { get; set; }
    */

}