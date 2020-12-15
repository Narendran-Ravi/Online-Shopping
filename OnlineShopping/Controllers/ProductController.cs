using System;
using System.Collections.Generic;               //Usage of IEnumerable type
using System.Linq;
using System.Web.Mvc;                           //Usage of Controllers as a base class   
using OnlineShopping.DomainModel;               //Usage of Database Model   
using OnlineShopping.ServiceLayer;              //Usage of Service Layer    
using OnlineShopping.ViewModels;                //Usage of ViewModel

namespace OnlineShopping.Controllers
{


    /// <summary>
    /// Product Controller : Has control over Add products, View the list of product in Admin perspective,Update the product details
    /// delete the product, Customer order list, View Customer details,Delete the completed order History
    /// </summary>
    public class ProductController : Controller
    {
        OnlineShoppingDbcontext onlineShoppingDbcontext = new OnlineShoppingDbcontext();
        IProductService productService;
        public ProductController()
        {
            productService = new ProductService();
         }

        public ActionResult AddProducts() //The seller can add new items using this method
        {
            if (Session["AdminEmail"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }

        [HttpPost]
        public ActionResult AddProducts(ProductViewModel productViewModel) //Postmethod for AddProducts
        {
            if (ModelState.IsValid)
            {
                productService.AddProduct(productViewModel);
                TempData["AddProduct"] = "Product added successfully,You can see the products added under View tab";
                return RedirectToAction("AddProducts"); 
            }
            else
              
            return View();
        }

        public ActionResult ViewProducts() //The admin can view the list of available products
        {
            if (Session["AdminEmail"] != null)
            {
                IEnumerable<Producttable> product = productService.ViewProducts();
                return View(product);
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }

        public ActionResult Update(int id) //the admin can update the details of the product
        {
            if (Session["AdminEmail"] != null)
            {
                ProductViewModel product = productService.GetId(id);
                return View(product);
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel productViewModel) //Post method for update method
        {
            if (ModelState.IsValid)
            {
                productService.Update(productViewModel);
                TempData["UpdateProduct"] = "Product Updated Successfully";
                return RedirectToAction("ViewProducts");
            }
            return View();
        }
        public ActionResult DeleteProduct(int id) //The admin can delete the respective item
        {
            if (Session["AdminEmail"] != null)
            {
                productService.DeleteProduct(id);
                return RedirectToAction("ViewProducts");
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }
      
        public ActionResult CustomerOrders() //The admin can view the details of customer orders
        {
            if (Session["AdminEmail"] != null)
            {
                IEnumerable<BuyRequest> buyRequest = productService.CustomerOrders();
                return View(buyRequest);
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }

        public ActionResult CustomerDetails(string email) //To get the details of the customer to dispatch the product
        {
            if (Session["AdminEmail"] != null)
            {
                Register register = productService.CustomerDetails(email);
                return View(register);
            }
            else
                return RedirectToAction("AdminLogin", "Account");
        }

        public ActionResult ProductSpecification(int id) //Displays the full specification details of a single product
        {
            if (Session["UserEmail"] != null)
            {
                Producttable producttable = productService.ProductSpecification(id);
                return View(producttable);
            }
            else
                return RedirectToAction("UserLogin", "Account");
        }

        public ActionResult DeleteOrder(int RequestID,int quantity,int productID) //The user can delete his order before it is approved by the admin
        {
            if (Session["UserEmail"] != null)
            {
                productService.DeleteOrder(RequestID);
                productService.AddStock(productID, quantity);
                return RedirectToAction("YourOrders", "User");
            }
            else
                return RedirectToAction("UserLogin", "Account");
        }
        public ActionResult CompletedOrder(int id) //This method removes the customer order detail
        {
            if (Session["AdminEmail"] != null)
            {
                CompletedOrders completedOrders = new CompletedOrders();
                BuyRequest buyRequest = onlineShoppingDbcontext.BuyRequests.Where(x => x.RequestId == id).FirstOrDefault();
                completedOrders.RequestId = buyRequest.RequestId;
                completedOrders.ProductId = buyRequest.ProductId;
                completedOrders.Email = buyRequest.Email;
                completedOrders.quantity = buyRequest.quantity;
                completedOrders.DateTime = DateTime.Now;
                onlineShoppingDbcontext.CompletedOrders.Add(completedOrders);
                onlineShoppingDbcontext.SaveChanges();
                productService.DeleteOrder(id);
                return RedirectToAction("CustomerOrders");
            }
            else
                return RedirectToAction("AdminLogin", "Account");
          }

        

      
    }
}