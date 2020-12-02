using System.Collections.Generic;               //Usage of IEnumerable type
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
        IProductService productService;
        public ProductController()
        {
            productService = new ProductService();
         }

        public ActionResult AddProducts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProducts(ProductViewModel productViewModel)
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

        public ActionResult ViewProducts()
        {
            IEnumerable<Producttable> product = productService.ViewProducts();
            return View(product);
        }

        public ActionResult Update(int id)
        {
            ProductViewModel product = productService.GetId(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productService.Update(productViewModel);
                TempData["UpdateProduct"] = "Product Updated Successfully";
                return RedirectToAction("ViewProducts");
            }
            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            productService.DeleteProduct(id);
            return RedirectToAction("ViewProducts");
        }

        public ActionResult CustomerOrders()
        {
            IEnumerable<BuyRequest> buyRequest = productService.CustomerOrders();
            return View(buyRequest);
        }

        public ActionResult CustomerDetails(string email)
        {
            Register register = productService.CustomerDetails(email);
            return View(register);

        }

        public ActionResult CompletedOrder(int id)
        {
            productService.CompletedOrder(id);
            return RedirectToAction("CustomerOrders");

        }

      
    }
}