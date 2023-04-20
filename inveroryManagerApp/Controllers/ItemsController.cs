using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace inveroryManagerApp.Controllers;

public partial class ItemsController : Controller
{

    public IActionResult ListItems()
    {
        DatabaseConnection db = new DatabaseConnection();
        List<ItemModel> listOfItems = db.GetListOfItems();
        ViewBag.Categories = db.GetItemCategoryModels(); 
        ViewBag.model = listOfItems;
        return View();
    }

    public delegate List<ItemModel> FilterDelegate(List<ItemModel> im);

    [HttpPost]
    public IActionResult ListItems(string filters)
    {
        DatabaseConnection db = new DatabaseConnection();
        FilterClass filterClass = new FilterClass();
        List<ItemModel> listOfItems = db.GetListOfItems();
        ViewBag.Categories = db.GetItemCategoryModels();

        if(filters == "Price: High to Low")
        {
            ViewBag.model = filterClass.PerformFilter(listOfItems, filterClass.PriceHighLow);
        }
        else if (filters == "Price: Low to High")
        {
            ViewBag.model = filterClass.PerformFilter(listOfItems, filterClass.PriceLowHigh);
        }
        else
        {
            ViewBag.model = listOfItems;
        }

        return View();
    }

    public class FilterClass
    {
        public FilterClass() { }

        public List<ItemModel> PerformFilter(List<ItemModel> items, FilterDelegate filter)
        {
            return filter.Invoke(items);
        }

        public List<ItemModel> PriceHighLow(List<ItemModel> list)
        {
            return list.OrderByDescending(x => x.SellPrice).ToList();
        }

        public List<ItemModel> PriceLowHigh(List<ItemModel> list)
        {
            return list.OrderBy(x => x.SellPrice).ToList();
        }
    }

    [HttpGet]
    public IActionResult AddCompany()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddCompany(CompanyModel company)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddCompany(company);
        return Redirect("AddItem");
    }

    [HttpGet]
    public IActionResult AddSupplier()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddSupplier(SupplierModel supplier)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddSupplier(supplier);
        return Redirect("AddItem");
    }

    [HttpGet]
    public IActionResult AddItemCategory()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddItemCategory(ItemCategoryModel itemCategory)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddItemCategory(itemCategory);
        return Redirect("AddItem");
    }

    [HttpGet]
    public IActionResult AddItem()
    {
        DatabaseConnection db = new DatabaseConnection();

        List<CompanyModel> companyList = db.GetListOfCompany();
        List<SelectListItem> companyItems = new List<SelectListItem>();
        companyItems.Add(new SelectListItem { Text = "--Select Company--", Value = "0" });
        foreach (CompanyModel companyItem in companyList)
        {
            companyItems.Add(new SelectListItem { Text = companyItem.CompanyName, Value = companyItem.CompanyId.ToString() });
        }
        ViewBag.listCompany = companyItems;

        List<SupplierModel> supplierList = db.GetListOfSupplier();
        List<SelectListItem> supplierItems = new List<SelectListItem>();
        supplierItems.Add(new SelectListItem { Text = "--Select Supplier--", Value = "0" });
        foreach (SupplierModel supplierItem in supplierList)
        {
            supplierItems.Add(new SelectListItem { Text = supplierItem.SupplierName, Value = supplierItem.SupplierId.ToString() });
        }
        ViewBag.listSupplier = supplierItems;

        List<ItemCategoryModel> itemCategoryList = db.GetItemCategoryModels();
        List<SelectListItem> itemCategoryItems = new List<SelectListItem>();
        itemCategoryItems.Add(new SelectListItem { Text = "--Select Item Category--", Value = "0" });
        foreach (ItemCategoryModel itemCategoryItem in itemCategoryList)
        {
            itemCategoryItems.Add(new SelectListItem { Text = itemCategoryItem.ItemCategoryName, Value = itemCategoryItem.ItemCategoryId.ToString() });
        }
        ViewBag.listItemCategory = itemCategoryItems;
        return View();
    }

    [HttpPost]
    public IActionResult AddItem(ItemModel item)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddItem(item);
        return Redirect("ListItems");
    }

    [HttpGet]
    public IActionResult ViewItem(int itemid)
    {
        DatabaseConnection db = new DatabaseConnection();
        ViewBag.model = db.GetItemByItemId(itemid);
        return View();
    }

}
