using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;

namespace inveroryManagerApp.Controllers;

public class OrderController : Controller 
{
    [HttpPost]
    public IActionResult CheckOrderStatus()
    {
        string name = Request.Form["Name"].ToString();
        ViewData["name"] = name;
        return View();
    }
}