using AutoMapper;
using System.Collections.Generic;
using OnlineShopping.ViewModels;
using OnlineShopping.Repositories;
using OnlineShopping.DomainModel;
using System.Linq;

namespace OnlineShopping.ServiceLayer
{
    public interface IProductService
    {
        /// <summary>
        /// interface IProductService which can be used a base class for the ProductService class.
        /// This interface contains the declaration of methods used in ProductService class.
        /// </summary>
       
        void AddProduct(ProductViewModel productViewModel);
        IEnumerable<Producttable> ViewProducts();
        ProductViewModel GetId(int id);
        void Update(ProductViewModel productViewModel);
        void DeleteProduct(int id);
       
        IEnumerable<BuyRequest> CustomerOrders();
        Register CustomerDetails(string email);
        Producttable ProductSpecification(int id);
        void DeleteOrder(int id);
        void AddStock(int id, int quantity);



    }
    public class ProductService:IProductService
    {

        /// <summary>
        /// Product Service contains the definition of methods to Add product detail,View the product details,Update the product,
        /// Delete the product,Buy a product, View customer details,View Customer orders, Delete completed orders by customers
        /// </summary>
        IProductRepository productRepository;
        public ProductService()  //constructor for Product Service class
        {
            productRepository = new ProductRepository();
        }
        public void AddProduct(ProductViewModel productViewModel) //Does the mapping function and passes the data to the Addproduct function in P
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Producttable>());
            IMapper mapper = config.CreateMapper();
            Producttable product = mapper.Map<ProductViewModel, Producttable>(productViewModel);
            productRepository.AddProduct(product);

        }

        public IEnumerable<Producttable> ViewProducts()  //calls the ViewProducts method in the product Repository
        {
             return productRepository.ViewProducts();
           
        }

        public ProductViewModel GetId(int id)  //does the mapping function and passes the id to the GetID method in Product Repository
        {
            Producttable product = productRepository.GetId(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Producttable, ProductViewModel>());
            var Mapper = config.CreateMapper();
            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);
            return productViewModel;
            
        }

        public void Update(ProductViewModel productViewModel)   //does the mapping function and calls the update function in product Repository
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel,Producttable > ());
            var Mapper = config.CreateMapper();
            Producttable producttable = Mapper.Map<Producttable>(productViewModel);
            productRepository.Update(producttable);
        }
        public void DeleteProduct(int id)   //passes the id to the deleteProduct function in product Repository
        {
            productRepository.DeleteProduct(id);
        }


        public IEnumerable<BuyRequest> CustomerOrders()  // calls the CustomerOrders function in the product repository
        {
            return productRepository.CustomerOrders();
        }

        public Register CustomerDetails(string email)    //passes the email to the product Repository
        {
            Register register = productRepository.CustomerDetails(email);
            return register;
        }

        public Producttable ProductSpecification(int id)  //passes the product id to the product Repository
        {
            return productRepository.ProductSpecification(id);
        }

        
        public void DeleteOrder(int id)    //passes the id to the Product Repository
        {
            productRepository.DeleteOrder(id);
        }

        public void AddStock(int id, int quantity)
        {
            OnlineShoppingDbcontext onlineShoppingDbcontext = new OnlineShoppingDbcontext();
            // IEnumerable<Producttable> producttables = productRepository.FindID(id);
            Producttable producttable = onlineShoppingDbcontext.Producttables.Find(id);
            //
            //bool IdExists = producttables.Any(x => x.ProductID == id);
            if (producttable!=null)
            {
                //foreach (var item in producttables)
                //{
                    if (producttable.ProductID == id)
                    {
                        producttable.Stock = (producttable.Stock + quantity);
                        productRepository.AddStock(producttable);
                    }
                //}
            }
        }
    }
}
