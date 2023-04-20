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

    public IActionResult Customers()
    {
        return View();
    }

    public IActionResult ListCustomers()
    {
        DatabaseConnection db = new DatabaseConnection();
        ViewBag.model = db.GetListOfCustomers();
        return View();
    }

    [HttpGet]
    public IActionResult EditCustomer(int customerid)
    {
        DatabaseConnection db = new DatabaseConnection();
        CustomerModel customerData = db.GetCustomerByCustomerId(customerid);
        TempData["StudentID"] = customerid;
        TempData.Keep();
        return View(customerData);
    }

    [HttpPost]
    public IActionResult EditCustomer(CustomerModel customer)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.EditCustomer(customer);
        return Redirect("ListCustomers");
    }

    [HttpGet]
    public IActionResult AddCustomer()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddCustomer(CustomerModel customer)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddCustomer(customer);
        return Redirect("ListCustomers");
    }

    public IActionResult Staffs()
    {
        return View();
    }

    public IActionResult ListStaffs()
    {
        DatabaseConnection db = new DatabaseConnection();
        ViewBag.model = db.GetListOfStaffs();
        return View();
    }

    [HttpGet]
    public IActionResult AddStaff()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddStaff(StaffModel staff)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddStaff(staff);
        return Redirect("ListStaffs");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
