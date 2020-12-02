using OnlineShopping.DomainModel;                 //Usage of Database Model
using OnlineShopping.ServiceLayer;                //Usage of Service Layer
using OnlineShopping.ViewModels;                  //Usage of ViewModels
using System;                                    //Usage of Convert.ToFunction
using System.Collections.Generic;                //Uasge of IEnumerable
using System.Web.Mvc;                            //Controller,ActionResult,TempData,RedirectToAction

namespace OnlineShopping.Controllers
{
    public class UserController : Controller
    {
        IProductService productService;
        IUserService userService;
        public UserController()
        {
            productService = new ProductService();
            userService = new UserService();
        }
        public ActionResult ViewProducts()
        {
            IEnumerable <Producttable> product = productService.ViewProducts();
            return View(product);
        }

        public ActionResult Buy(int id)
        {
            if (Session["UserEmail"] != null)
            {
                BuyRequest buyRequest = new BuyRequest();
                buyRequest.ProductId = id;
                buyRequest.Email = Convert.ToString(Session["UserEmail"]);
                productService.Buy(buyRequest);
            }
            else
            {
                TempData["Login"] = "Please Login Before buying a Product";
                return RedirectToAction("UserLogin","Account");
            }
            TempData["BuyRequest"] = "Your Buy Request has been sent to the Seller";
            return RedirectToAction("ViewProducts");
        }

        public ActionResult UpdateProfile()
        {
            var email = Convert.ToString(Session["UserEmail"]);
            RegisterViewModel registerViewModel = userService.GetEmail(email);
            return View(registerViewModel);
        }

        [HttpPost]
        public ActionResult UpdateProfile(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                userService.UpdateProfile(registerViewModel);
                TempData["UpdateProfile"] = "Your Profile has been Updated Successfully";
                return RedirectToAction("ViewProducts", "User");

            }
            else
            return View(registerViewModel);
           
        }
    }
}