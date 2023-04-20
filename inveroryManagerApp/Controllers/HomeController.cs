using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace inveroryManagerApp.Controllers;

public partial class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Orders()
    {
        return View();
    }

    public IActionResult Items()
    {
        return View();
    }

    public IActionResult ListItems()
    {
        DatabaseConnection db = new DatabaseConnection();
        List<ItemModel> listOfItems = db.GetListOfItems();
        ViewBag.model = listOfItems;
        return View();
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
    public ActionResult AddItem(ItemModel item)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddItem(item);
        return Redirect("ListItems");
    }

    public IActionResult OrderStatus()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
