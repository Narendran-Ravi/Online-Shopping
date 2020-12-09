using OnlineShopping.ViewModels;            //Usage of View Models  
using OnlineShopping.DomainModel;           //Usage of Database Models
using System.Collections.Generic;           //Usage of IEnumerable
using System.Data.Entity;                   //Usage of Entity state
using System.Linq;                          //Usage of where tags


namespace OnlineShopping.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// IUserRepository - Interface used as a base for the UserRepository class
        /// </summary>
        IEnumerable<Register> UserLogin(UserViewModel userViewModel);
        Register GetEmail(string email);
        void UpdateProfile(Register register);
        void Buy(BuyRequest buyRequest);
        void RemoveCartItem(int id);
        IEnumerable<BuyRequest> YourOrders(string email);
        IEnumerable<CompletedOrders> AlreadyBought(string email);
        IEnumerable<Cart> ViewCart(string email);
        IEnumerable<Producttable> FindID(int id);
        void AddCart(Cart cart);

    }
    public class UserRepository:IUserRepository
    {

        /// <summary>
        /// Establishes the connection with the Registers Table in the OnlineShoppingDb database
        /// </summary>
        OnlineShoppingDbcontext onlineShoppingDbcontext;

        public UserRepository() //constructor for UserRepository
        {
            onlineShoppingDbcontext = new OnlineShoppingDbcontext();
        }
        public IEnumerable<Register> UserLogin(UserViewModel userLoginViewModel)    // checks whether the user enters the correct username and password
        {
            return  onlineShoppingDbcontext.Registers.Where(m => m.Email == userLoginViewModel.Email && m.Password == userLoginViewModel.Password).ToList();

        }

        public Register GetEmail(string email)    //Gets the required email from the registers table
        {
            var data = onlineShoppingDbcontext.Registers.Where(x => x.Email == email).SingleOrDefault();
            return data;
        }

        public void UpdateProfile(Register register)    //Updates the Register table 
        {
            onlineShoppingDbcontext.Entry(register).State =EntityState.Modified;
            onlineShoppingDbcontext.SaveChanges();
        }

        public void Buy(BuyRequest buyRequest)
        {
            onlineShoppingDbcontext.BuyRequests.Add(buyRequest);
            onlineShoppingDbcontext.SaveChanges();
        }

        public void RemoveCartItem(int id)
        {
            var res = onlineShoppingDbcontext.Carts.Find(id);
            onlineShoppingDbcontext.Carts.Remove(res);
            onlineShoppingDbcontext.SaveChanges();
        }
        public IEnumerable<BuyRequest> YourOrders(string email)
        {
            return onlineShoppingDbcontext.BuyRequests.Where(x => x.Email == email).ToList();
        }

        public IEnumerable<CompletedOrders> AlreadyBought(string email)
        {
            return onlineShoppingDbcontext.CompletedOrders.Where(x => x.Email == email).ToList();
        }

        public IEnumerable<Cart> ViewCart(string email)
        {
            return onlineShoppingDbcontext.Carts.Where(x => x.Email == email).ToList();
        }

        public IEnumerable<Producttable> FindID(int id)
        {
            return onlineShoppingDbcontext.Producttables.Where(x => x.ProductID == id).ToList();
        }

        public void AddCart(Cart cart)
        {
            onlineShoppingDbcontext.Carts.Add(cart);
            onlineShoppingDbcontext.SaveChanges();
        }

    }
}
