using System.Web.Mvc;                 //Usage of Controllers

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Home controller - Home page for the website and Contact information  page
        /// </summary>
        
       
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}