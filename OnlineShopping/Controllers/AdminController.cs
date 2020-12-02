using OnlineShopping.DomainModel;
using OnlineShopping.ServiceLayer;
using System;                           //Usage of Convert.ToFunction
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
        public ActionResult UpdateProfile()
        {
            var res = Convert.ToString(Session["AdminEmail"]);
            Admin admin = adminService.GetEmail(res);
            return View(admin);
        }

        //Post method for Admin Update Profile
        [HttpPost]
        public ActionResult UpdateProfile(Admin admin)
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
    }
}