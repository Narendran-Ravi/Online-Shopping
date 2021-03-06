﻿using System.Web.Mvc;                       //Usage of Controllers
using OnlineShopping.ViewModels;            //Usage of ViewModels
using OnlineShopping.ServiceLayer;          //Usage of ServiceLayer
using OnlineShopping.DomainModel;           //Usage of Database Model
using System;

namespace OnlineShopping.Controllers
{/// <summary>
/// This controller controlls the register and login module for user and admin
/// </summary>
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
        
        public ActionResult Register() //Get method for sign up
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel) //Post method for sign up
        {
            try
            {
                Register register = registerService.Mapp(registerViewModel);
                registerService.AlreadyExisting(register, out bool email, out bool phoneNumber);
                if (email)
                {
                    ModelState.AddModelError("", "Email Already Exists");
                    return View();
                }
                else if (phoneNumber)
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
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Register", "Account"));
            }
        }

        public ActionResult UserLogin() //Get method for userLogin
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(UserViewModel userViewModel) //Post method for UserLogin
        {
            try
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
                        ModelState.AddModelError("", "Invalid UserEmail and Password");
                        return View(userViewModel);
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "UserLogin", "Account"));
            }
        }

        public ActionResult AdminLogin()  //Get method for admin Login
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminViewModel adminViewModel)   //Post method for admin Login
        {
            try
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
                        ModelState.AddModelError("", "Invalid User Email and Password ");
                        return View(adminViewModel);
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "AdminLogin", "Account"));
            }
        }
        public ActionResult Logout()
        {
           
            Session["UserEmail"] = null;
            Session["AdminEmail"] = null;
            return View("UserLogin");
        }
    }
}