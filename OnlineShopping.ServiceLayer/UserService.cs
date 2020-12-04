using AutoMapper;                                    //Usage of AutoMapper functions
using OnlineShopping.DomainModel;                    //Usage of Database Model
using OnlineShopping.ViewModels;                     //Usage of View Model
using OnlineShopping.Repositories;                   //Usage of Repositories
using System.Linq;                                  //Usage of First or default function

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
        void RemoveCartItem(int id);
    }
    public class UserService:IUserService
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
                var config = new MapperConfiguration(cfg =>  cfg.CreateMap<Register, UserViewModel>()); 
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

        public void RemoveCartItem(int id)
        {
            userRepository.RemoveCartItem(id);
        }
    }
}
