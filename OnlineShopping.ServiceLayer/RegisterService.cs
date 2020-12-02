using AutoMapper;                       //Usage of Mapping functions
using OnlineShopping.ViewModels;        //Usage of ViewModel
using OnlineShopping.DomainModel;       //Usage of Database Model
using OnlineShopping.Repositories;      //Usage of Repository

namespace OnlineShopping.ServiceLayer
{
    public interface IRegisterService
    {
        /// <summary>
        /// IRegisterService - Interface used as a base for the RegisterService class
        /// </summary>
        void Register(RegisterViewModel registerViewModel);
        Register Mapp(RegisterViewModel registerViewModel);
        void AlreadyExisting(Register register, out bool email, out bool phoneNumber);
    }
    public class RegisterService:IRegisterService
    {
        /// <summary>
        /// UserService contains the mapper functions for modules like Register,AlreadyExisting Function
        /// </summary>
        
        IRegisterRepository registerRepository;

        public RegisterService()   // constructor for registerservicce
        {
        registerRepository= new RegisterRepository();
        }
        public void Register(RegisterViewModel registerViewModel)   //Mapping function for Register module
        {
          
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, Register>());  
            IMapper mapper = config.CreateMapper();
            Register register = mapper.Map<RegisterViewModel, Register>(registerViewModel);
            registerRepository.Register(register);
        }

        public Register Mapp(RegisterViewModel registerViewModel) //Mapping function
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterViewModel, Register>());
            IMapper mapper = config.CreateMapper();
            Register register = mapper.Map<RegisterViewModel, Register>(registerViewModel);
            return register;
            
        }

        public void AlreadyExisting(Register register,out bool email,out bool phoneNumber)  //check already existing email and phone number
        {
            registerRepository.AlreadyExisting(register, out email, out phoneNumber);

        }
    }
}
