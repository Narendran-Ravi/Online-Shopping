using OnlineShopping.DomainModel;       //Usage of Database Model
using OnlineShopping.ServiceLayer;      //Usage of Service Layer
using System;                           //Usage of Convert.ToFunction
using System.Collections.Generic;        //Usage of IEnumerable
using System.Web.Mvc;                   //Controllers

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// Admin Controller - This controller has the control to change admin Profile and Admin Home page
    /// </summary>
   

    public class AdminController : Controller
    {

        IAdminService adminService;

        public AdminController()    //constructor for the Admin controller 
        {
            adminService = new AdminService();
        }

        //Home page for Admin
        public ActionResult Home()
        {
            return View();
        }

        //Get method for Admin Update Profile
        public ActionResult UpdateProfile() //The admin can update his profile in this method
        {
            if (Session["AdminEmail"] != null)
            {
                var res = Convert.ToString(Session["AdminEmail"]);
                Admin admin = adminService.GetEmail(res);
                return View(admin);
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }

        //Post method for Admin Update Profile
        [HttpPost]
        public ActionResult UpdateProfile(Admin admin) //Post method for UpdateProfile
        {
            if (ModelState.IsValid)
            {
                adminService.UpdateProfile(admin);
                TempData["AdminUpdate"] = "Your Details have been Updated";
                return RedirectToAction("Home", "Admin");
            }
            else
                return View(admin);
            
        }

        public ActionResult CompletedOrders() //This method displays the completed order
        {
            if (Session["AdminEmail"] != null)
            {
                IEnumerable<CompletedOrders> completedOrders = adminService.CompletedOrders();
                return View(completedOrders);
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }
    }
}