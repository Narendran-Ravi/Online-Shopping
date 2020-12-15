using AutoMapper;                                    //Usage of AutoMapper functions
using OnlineShopping.DomainModel;                    //Usage of Database Model
using OnlineShopping.ViewModels;                     //Usage of View Model
using OnlineShopping.Repositories;                   //Usage of Repositories
using System.Linq;                                  //Usage of First or default function
using System.Collections.Generic;

namespace OnlineShopping.ServiceLayer
{
    public interface IUserService
    {  
        /// <summary>
        /// IUserService - Interface used as a base for the UserService class
        /// </summary>
       
        UserViewModel UserLogin(UserViewModel userViewModel);
        RegisterViewModel GetEmail(string email);
        void UpdateProfile(RegisterViewModel registerViewModel);
        bool Buy(int id, string email, int quantity);
        void RemoveCartItem(int id);
        IEnumerable<BuyRequest> YourOrders(string email);
        IEnumerable<CompletedOrders> AlreadyBought(string email);
        IEnumerable<Cart> ViewCart(string email);
        IEnumerable<Producttable> FindID(int id);
        void AddCart(int id, string email);
        bool CartBuy(int id, string email, int quantity);
        bool CheckAvailability(int id, int quantity);
        void AddCartQuantity(int productID, string user);
        void AddQuantity(int productID);
        void SubtractCartQuantity(int productID, string user);
        void SubtractQuantity(int productID);
        void StockUpdate(int id, int quantity);
    }
    public class UserService : IUserService
    {

        /// <summary>
        /// UserService contains the mapper functions for modules like Userlogin,GetEmail and Update Profile
        /// </summary>



        IUserRepository userRepository;

        public UserService()     // constructor for userService
        {
            userRepository = new UserRepository();
        }
        public UserViewModel UserLogin(UserViewModel userViewModel)    // contains mapper function and passes the control to repository to check email and password matches
        {
            Register fetchedData = userRepository.UserLogin(userViewModel).FirstOrDefault();
            UserViewModel sensitiveData = null;
            if (fetchedData != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Register, UserViewModel>());
                IMapper mapper = config.CreateMapper();
                sensitiveData = mapper.Map<Register, UserViewModel>(fetchedData);

            }
            return sensitiveData;
        }

        public RegisterViewModel GetEmail(string email)     //mapper function and passes to respository to check email exists in database
        {
            Register register = userRepository.GetEmail(email);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Register, RegisterViewModel>());
            IMapper mapper = config.CreateMapper();
            RegisterViewModel registerViewModel = mapper.Map<RegisterViewModel>(register);
            return registerViewModel;
        }

        public void UpdateProfile(RegisterViewModel registerViewModel)   //mapper function and passes to repository to Update the userprofile
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, Register>());
            IMapper mapper = config.CreateMapper();
            Register register = mapper.Map<Register>(registerViewModel);
            userRepository.UpdateProfile(register);
        }

        public bool Buy(int id, string email,int quantity)
        {
            BuyRequest buyRequest = new BuyRequest();
            bool AvailabilityCheck = CheckAvailability(id, quantity);
            if (AvailabilityCheck)
            {
                buyRequest.ProductId = id;
                buyRequest.Email = email;
                buyRequest.quantity = quantity;
                userRepository.Buy(buyRequest);
                StockUpdate(id, buyRequest.quantity);
                return true;
            }
            return false;
        }

        public void RemoveCartItem(int id)
        {
            userRepository.RemoveCartItem(id);
        }

        public IEnumerable<BuyRequest> YourOrders(string email)
        {
            return userRepository.YourOrders(email);
        }
        public IEnumerable<CompletedOrders> AlreadyBought(string email)
        {
            return userRepository.AlreadyBought(email);
        }

        public IEnumerable<Cart> ViewCart(string email)
        {
            return userRepository.ViewCart(email);
        }

        public IEnumerable<Producttable> FindID(int id)
        {
            return userRepository.FindID(id);
        }

        public void AddCart(int productID, string user)
        {
            Cart cart = new Cart();
            List<Cart> carts = userRepository.FindCartUser(user);
            bool AlreadyExists = carts.Any(x => x.ProductID == productID);
            if (AlreadyExists)
            {
                foreach (var item in carts)
                {
                    if (item.ProductID == productID)
                    {
                        item.quantity = ++item.quantity;
                        userRepository.UpdateCart(item);
                    }
                }
            }
            else
            {
                cart.quantity = 1;
                cart.ProductID = productID;
                cart.Email = user;
                userRepository.AddCart(cart);
            }
        }

        public bool CartBuy(int id, string email, int quantity)
        {
            BuyRequest buyRequest = new BuyRequest();
            bool checkAvailability = CheckAvailability(id, quantity);
            if (checkAvailability)
            {
                buyRequest.ProductId = id;
                buyRequest.Email = email;
                buyRequest.quantity = quantity;
                userRepository.Buy(buyRequest);
                StockUpdate(id, quantity);
                return true;
            }
            return false;
                
           
        }

        public bool CheckAvailability(int id, int quantity)
        {
            Producttable producttable = new Producttable();
            IEnumerable<Producttable> producttables = userRepository.FindID(id);
            bool IdExists = producttables.Any(x => x.ProductID == id);
            if (IdExists)
            {
                foreach (var item in producttables)
                {
                    if (item.ProductID == id)
                    {
                        if (quantity <= item.Stock)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
            }
            return false;
        }
        public void StockUpdate(int id,int quantity)
        {
            Producttable producttable = new Producttable();
            IEnumerable<Producttable> producttables = userRepository.FindID(id);
            bool IdExists = producttables.Any(x => x.ProductID == id);
            if(IdExists)
            {
                foreach(var item in producttables)
                {
                    if(item.ProductID == id)
                    {
                        item.Stock = (item.Stock - quantity);
                        userRepository.StockUpdate(item);
                     }
                }
            }
        }
        public void AddCartQuantity(int productID, string user)
        {

            Cart cart = new Cart();
            List<Cart> carts = userRepository.FindCartUser(user);
            bool AlreadyExists = carts.Any(x => x.ProductID == productID);
            if (AlreadyExists)
            {
                foreach (var item in carts)
                {
                    if (item.ProductID == productID)
                    {
                        item.quantity = ++item.quantity;
                        userRepository.UpdateCart(item);
                    }
                }
            }
        }

        public void AddQuantity(int productID)
        {
            Producttable producttable = new Producttable();
            IEnumerable<Producttable> producttables = userRepository.FindID(productID);
            bool IdExists = producttables.Any(x => x.ProductID == productID);
            if (IdExists)
            {
                foreach(var item in producttables)
                {
                    if(item.ProductID==productID)
                    {
                        item.quantity = ++item.quantity;
                        userRepository.StockUpdate(item);
                    }
                }
            }
        }

        public void SubtractCartQuantity(int productID, string user)
        {
            Cart cart = new Cart();
            List<Cart> carts = userRepository.FindCartUser(user);
            bool AlreadyExists = carts.Any(x => x.ProductID == productID);
            if (AlreadyExists)
            {
                foreach (var item in carts)
                {
                    if (item.ProductID == productID)
                    {
                        item.quantity = --item.quantity;
                        userRepository.UpdateCart(item);
                    }
                }
            }
        }

        public void SubtractQuantity(int productID)
        {
            Producttable producttable = new Producttable();
            IEnumerable<Producttable> producttables = userRepository.FindID(productID);
            bool IdExists = producttables.Any(x => x.ProductID == productID);
            if (IdExists)
            {
                foreach (var item in producttables)
                {
                    if (item.ProductID == productID)
                    {
                        item.quantity = --item.quantity;
                        userRepository.StockUpdate(item);
                    }
                }
            }
        }

    }
}
