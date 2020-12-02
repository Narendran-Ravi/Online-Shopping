using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopping.DomainModel;
using OnlineShopping.ViewModels;

namespace OnlineShopping.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Producttable product);
        IEnumerable<Producttable> ViewProducts();
        Producttable GetId(int id);
        void Update(Producttable producttable);
        void DeleteProduct(int id);
        void Buy(BuyRequest buyRequest);
        IEnumerable<BuyRequest> CustomerOrders();
        Register CustomerDetails(string email);
        void CompletedOrder(int id);

    }
    public class ProductRepository:IProductRepository
    {
        OnlineShoppingDbcontext onlineShoppingDbcontext;
       public ProductRepository()
        {
           onlineShoppingDbcontext = new OnlineShoppingDbcontext();
        }

      

        public void AddProduct(Producttable product)
        {
            onlineShoppingDbcontext.Producttables.Add(product);
            onlineShoppingDbcontext.SaveChanges();

        }

        public IEnumerable<Producttable> ViewProducts()
        {
            IEnumerable<Producttable> product = onlineShoppingDbcontext.Producttables.ToList();
            return product;
        }

        public Producttable GetId(int id)
        {
            return onlineShoppingDbcontext.Producttables.Find(id);
        }

        public void Update(Producttable producttable)
        {
            onlineShoppingDbcontext.Entry(producttable).State = EntityState.Modified;
            onlineShoppingDbcontext.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            var res = onlineShoppingDbcontext.Producttables.Find(id);
            onlineShoppingDbcontext.Producttables.Remove(res);
            onlineShoppingDbcontext.SaveChanges();

        }

        public void Buy(BuyRequest buyRequest)
        {
            onlineShoppingDbcontext.BuyRequests.Add(buyRequest);
            onlineShoppingDbcontext.SaveChanges();
        }
        public IEnumerable<BuyRequest> CustomerOrders()
        {
            IEnumerable<BuyRequest> buyRequest = onlineShoppingDbcontext.BuyRequests.ToList();
            return buyRequest;
        }

        public Register CustomerDetails(string email)
        {
            var data = onlineShoppingDbcontext.Registers.Where(x => x.Email == email).SingleOrDefault();
            return data;
        }

        public void CompletedOrder(int id)
        {
            var res = onlineShoppingDbcontext.BuyRequests.Find(id);
            onlineShoppingDbcontext.BuyRequests.Remove(res);
            onlineShoppingDbcontext.SaveChanges();
        }
    }
}
