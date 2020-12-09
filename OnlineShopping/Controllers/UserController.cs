using OnlineShopping.DomainModel;                 //Usage of Database Model
using OnlineShopping.ServiceLayer;                //Usage of Service Layer
using OnlineShopping.ViewModels;                  //Usage of ViewModels
using System;                                    //Usage of Convert.ToFunction
using System.Collections.Generic;                //Uasge of IEnumerable
using System.Linq;
using System.Web.Mvc;                            //Controller,ActionResult,TempData,RedirectToAction

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// User Controller - This controller contains the functions that can be performed by the user  
    /// </summary>
    public class UserController : Controller
    {
        OnlineShoppingDbcontext onlineShoppingDbcontext = new OnlineShoppingDbcontext();
        IProductService productService;
        IUserService userService;
        public UserController()
        {
            productService = new ProductService();
            userService = new UserService();
        }
        public ActionResult ViewProducts() //This method displays the list of products available
        {
            IEnumerable <Producttable> product = productService.ViewProducts();
            return View(product);
        }

        public ActionResult Buy(int id)  //Using this method we can buy directly from the shop page
        {
            if (Session["UserEmail"] != null)
            {
                string email= Convert.ToString(Session["UserEmail"]);
                userService.Buy(id, email);
                
            }
            else
            {
                TempData["Login"] = "Please Login Before buying a Product";
                return RedirectToAction("UserLogin","Account");
            }
            TempData["BuyRequest"] = "Your Buy Request has been sent to the Seller";
            return RedirectToAction("ViewProducts");
        }

        public ActionResult UpdateProfile() //Get method for Update Profile
        {
            if (Session["UserEmail"] != null)
            {
                var email = Convert.ToString(Session["UserEmail"]);
                RegisterViewModel registerViewModel = userService.GetEmail(email);
                return View(registerViewModel);
            }
            else
                return RedirectToAction("UserLogin", "Account");
        }

        [HttpPost]
        public ActionResult UpdateProfile(RegisterViewModel registerViewModel) //Post method for UpdateProfile
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

        public ActionResult YourOrders() //This method shows the list of items that need to be approved by the seller
        {
            if (Session["UserEmail"] != null)
            {
                string email = Convert.ToString(Session["UserEmail"]);
                IEnumerable<BuyRequest> yourorders = userService.YourOrders(email);
                return View(yourorders);
            }
            else
                return RedirectToAction("UserLogin", "Account");
        }

        public ActionResult AlreadyBought() //This method displays the list of products already bought by the user
        {
            if (Session["UserEmail"] != null)
            {
                string email = Convert.ToString(Session["UserEmail"]);
                IEnumerable<CompletedOrders> completedOrders = userService.AlreadyBought(email);
                return View(completedOrders);
            }
            else
                return RedirectToAction("UserLogin", "Account");
        }

        public ActionResult AddCart(int id) //This method is used to add the items to the cart by the user
        {
            
            if (Session["UserEmail"] != null)
            {
                string email = Convert.ToString(Session["UserEmail"]);
               
                IEnumerable<Producttable> producttable = userService.FindID(id);
                userService.AddCart(id, email);
                
               
            }
            else
            {
                TempData["AddLogin"] = "Please Login before adding the items to your cart";
                return RedirectToAction("UserLogin", "Account");
            }
            TempData["CartSuccess"] = "The item has been successfully added to your cart";
            return RedirectToAction("ViewProducts", "User");
        }

        public ActionResult ViewCart()  //This method displays the items available in a respective user cart
        {
            if (Session["UserEmail"] != null)
            {
                string email = Convert.ToString(Session["UserEmail"]);
                IEnumerable<Cart> cart = userService.ViewCart(email); 
                return View(cart);
            }
            else
            
                TempData["ViewCartError"] = "Please Login to view the Items in the cart";
                return RedirectToAction("UserLogin", "Account");
            
        }
        public ActionResult CartBuy(int id,int quantity) //This method is used to buy the products from the cart
        {
            if (Session["UserEmail"] != null)
            {
                string email = Convert.ToString(Session["UserEmail"]);
                userService.CartBuy(id, email, quantity);


            }
            else
            {

                TempData["ViewCartError"] = "Please Login to view the Items in the cart";
                return RedirectToAction("UserLogin", "Account");
            }

            TempData["CartBuyRequest"] = "Your Buy Request has been sent to the Seller";
            return RedirectToAction("ViewCart");
        }

        public ActionResult RemoveCartItem(int id)   //This method removes the item from the cart
        {
            userService.RemoveCartItem(id);
            return RedirectToAction("ViewCart");
        }
    }
}