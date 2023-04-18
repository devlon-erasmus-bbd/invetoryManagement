using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;

namespace inveroryManagerApp.Controllers;

public class HomeController : Controller
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
        List<ItemModel> listOfItems = db.ListOfItems();
        ViewBag.model = listOfItems;
        return View();
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
