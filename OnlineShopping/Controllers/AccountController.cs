using System.Web.Mvc;                       //Usage of Controllers
using OnlineShopping.ViewModels;            //Usage of ViewModels
using OnlineShopping.ServiceLayer;          //Usage of ServiceLayer
using OnlineShopping.DomainModel;           //Usage of Database Model

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {
        IRegisterService registerService;
        IUserService userService;
        IAdminService adminService;

        public AccountController()
        {
            registerService = new RegisterService();
            userService = new UserService();
            adminService = new AdminService();
        }
        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            Register register = registerService.Mapp(registerViewModel);
            registerService.AlreadyExisting(register, out bool email, out bool phoneNumber);
            if(email)
            {
                ModelState.AddModelError("", "Email Already Exists");
                return View();
            }
            else if(phoneNumber)
            {
                ModelState.AddModelError("", "phoneNumber Already Exists");
            }
            if (ModelState.IsValid)
            {
                registerService.Register(registerViewModel);
                TempData["RegisterSuccessful"] = "You have successfully registered";
                return RedirectToAction("UserLogin");
            }
            else
                return View();
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                UserViewModel validateData = userService.UserLogin(userViewModel);
                if (validateData != null)
                {
                    Session["UserEmail"] = userViewModel.Email;
                    return RedirectToAction("ViewProducts", "User");
                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid UserEmail and Password");
                    return View(userViewModel);
                }
            }
            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminViewModel adminViewModel)
        {
            if (ModelState.IsValid)
            {
                AdminViewModel validateData = adminService.AdminLogin(adminViewModel);
                if (validateData != null)
                {
                    Session["AdminEmail"] = adminViewModel.Email;
                    return RedirectToAction("ViewProducts", "Product");
                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid User Email and Password ");
                    return View(adminViewModel);
                }
            }
            return View();

        }
        public ActionResult Logout()
        {
           
            Session["UserEmail"] = null;
            Session["AdminEmail"] = null;
            return View("UserLogin");
        }
    }
}