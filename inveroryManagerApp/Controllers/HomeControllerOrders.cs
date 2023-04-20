using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;

namespace inveroryManagerApp.Controllers;

public partial class HomeController : Controller
{
    public IActionResult ListOrders()
    {
        DatabaseConnection db = new DatabaseConnection();
        List<OrderModel> listOfOrders = db.GetOrders();
        ViewBag.model = listOfOrders;
        return View();
    }

    [HttpGet]
    public IActionResult AddOrder()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddOrder(OrderModel order)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddOrder(order);
        return Redirect("ListOrders");
    }

    public IActionResult OrderStatus()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CheckOrderStatus()
    {
        string name = Request.Form["Name"].ToString();
        ViewData["name"] = name;
        return View();
    }
}