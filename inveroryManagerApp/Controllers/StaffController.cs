using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using inveroryManagerApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace inveroryManagerApp.Controllers;

public partial class StaffController : Controller
{
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
}