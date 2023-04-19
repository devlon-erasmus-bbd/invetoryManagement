using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;

namespace inveroryManagerApp.Controllers;

public partial class HomeController : Controller
{
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