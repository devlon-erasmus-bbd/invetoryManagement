using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace inveroryManagerApp.Controllers;

public partial class CustomersController : Controller
{
    public IActionResult ListCustomers()
    {
        DatabaseConnection db = new DatabaseConnection();
        ViewBag.model = db.GetListOfCustomers();
        return View();
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

    [HttpGet]
    public IActionResult EditCustomer(int customerid)
    {
        DatabaseConnection db = new DatabaseConnection();
        CustomerModel customerData = db.GetCustomerByCustomerId(customerid);
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
    public IActionResult BuyItems()
    {
        DatabaseConnection db = new DatabaseConnection();

        List<CustomerModel> customerList = db.GetListOfCustomers();
        List<SelectListItem> customerItems = new List<SelectListItem>();
        customerItems.Add(new SelectListItem { Text = "--Select Customer--", Value = "0" });
        foreach (CustomerModel customerItem in customerList)
        {
            customerItems.Add(new SelectListItem { Text = customerItem.CustomerName, Value = customerItem.CustomerId.ToString() });
        }
        ViewBag.listCustomer = customerItems;

        List<StaffModel> staffList = db.GetListOfStaffs();
        List<SelectListItem> staffItems = new List<SelectListItem>();
        staffItems.Add(new SelectListItem { Text = "--Select Staff--", Value = "0" });
        foreach (StaffModel staffItem in staffList)
        {
            staffItems.Add(new SelectListItem { Text = staffItem.StaffName, Value = staffItem.StaffId.ToString() });
        }
        ViewBag.listStaff = staffItems;

        List<ItemModel> itemList = db.GetListOfItems();
        List<SelectListItem> itemItems = new List<SelectListItem>();
        //itemItems.Add(new SelectListItem { Text = "--Select Item--", Value = "0" });
        foreach (ItemModel itemItem in itemList)
        {
            itemItems.Add(new SelectListItem { Text = itemItem.ItemName + " (" + itemItem.SellPrice + ")", Value = itemItem.ItemId.ToString() });
        }
        ViewBag.listItem = itemItems;

        BuyItemModel objStudentModel = new BuyItemModel();
        objStudentModel.listItem = itemItems;

        return View();
    }

    [HttpPost]
    public IActionResult BuyItems(BuyItemModel buyItem)
    {
        DatabaseConnection db = new DatabaseConnection();
        db.AddTransaction(buyItem);
        return Redirect("ListTransactions");
    }

    public IActionResult ListTransactions()
    {
        DatabaseConnection db = new DatabaseConnection();
        ViewBag.model = db.GetTransactions();
        return View();
    }

    public IActionResult ViewTransactions(int transactionid)
    {
        DatabaseConnection db = new DatabaseConnection();
        ViewBag.model = db.GetTransactionDetailsByTransactionId(transactionid);
        ViewBag.transaction = db.GetTransactionByTransactionId(transactionid);
        return View();
    }
}