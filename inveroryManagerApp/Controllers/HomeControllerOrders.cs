// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using inveroryManagerApp.Models;

// namespace inveroryManagerApp.Controllers;

// public partial class HomeController : Controller
// {
//     public IActionResult ListOrders()
//     {
//         DatabaseConnection db = new DatabaseConnection();
//         List<OrderModel> listOfOrders = db.GetOrders();
//         ViewBag.model = listOfOrders;
//         return View();
//     }

//     [HttpGet]
//     public IActionResult AddOrder()
//     {
//         return View();
//     }

//     [HttpPost]
//     public IActionResult AddOrder(OrderModel order)
//     {
//         DatabaseConnection db = new DatabaseConnection();
//         db.AddOrder(order);
//         return Redirect("ListOrders");
//     }

//     [HttpGet]
//     public IActionResult OrderStatus()
//     {
//         return View();
//     }

//     [HttpPost]
//     public IActionResult OrderStatus(OrderModel order)
//     {
//         TempData["model"] = order.OrderId;
//         return RedirectToAction("DisplayOrderStatus");
//     }

//     public IActionResult DisplayOrderStatus()
//     {
//         //Get data for ID
//         ViewBag.model = TempData["model"];
//         return View();
//     }
// }