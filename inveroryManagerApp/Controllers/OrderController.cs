using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace inveroryManagerApp.Controllers;

public partial class OrdersController : Controller
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
        DatabaseConnection db = new DatabaseConnection();
        List<ItemModel> itemList = db.GetListOfItems();
        List<SelectListItem> itemItems = new List<SelectListItem>();
        itemItems.Add(new SelectListItem { Text = "--Select Item--", Value = "0" });
        foreach (ItemModel itemItem in itemList)
        {
            itemItems.Add(new SelectListItem { Text = itemItem.ItemName, Value = itemItem.ItemId.ToString() });
        }
        ViewBag.listItem = itemItems;
        return View();
    }

    [HttpPost]
    public IActionResult AddOrder(OrderModel order)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddOrder(order);
        return Redirect("ListOrders");
    }

    [HttpGet]
    public IActionResult OrderStatus()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DisplayOrderStatus(OrderModel order)
    {
        DatabaseConnection db = new DatabaseConnection();
        order = db.GetOrderById(order.OrderId);
        ViewBag.model = order;
        if (order == null)
            return NoContent();
        return View();
    }
}