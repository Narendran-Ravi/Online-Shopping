using AutoMapper;                           //Mapper function
using OnlineShopping.DomainModel;           //Usage of Database Model
using OnlineShopping.ViewModels;            //Usage of ViewModels
using OnlineShopping.Repositories;          //Usage of Repositories
using System.Linq;                          //Usage of IEnumerable tag


namespace OnlineShopping.ServiceLayer
{
    public interface IAdminService
    {

        /// <summary>
        /// interface IAdminService can be used as a base class for AdminService class
        /// This interface contains the declaration of the methods used in AdminService class 
        /// </summary>
       
        AdminViewModel AdminLogin(AdminViewModel adminViewModel);
        Admin GetEmail(string email);
        void UpdateProfile(Admin admin);

    }
    public class AdminService:IAdminService
    {
        /// <summary>
        ///This class contains definition for methods like AdminLogin, GetEmail  and UpdateProfile for admin 
        /// </summary>
        
        IAdminRepository adminRepository;
        public AdminService()     //constructor for AdminService class
        {
            adminRepository = new AdminRepository();
        }
        public AdminViewModel AdminLogin(AdminViewModel adminViewModel)  //Gets the data from the Admin REpository and does the mapping function annd 
        {
            Admin fetchedData = adminRepository.AdminLogin(adminViewModel).FirstOrDefault();
            AdminViewModel sensitiveData = null;
            if (fetchedData != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Admin, AdminViewModel>());
                IMapper mapper = config.CreateMapper();
                sensitiveData = mapper.Map<Admin, AdminViewModel>(fetchedData);
            }
            return sensitiveData;

        }

        public Admin GetEmail(string email)      //passes the email to the GetEmail method to the Admin Repository
        {
            Admin admin = adminRepository.GetEmail(email);
            return admin;
        }

        public void UpdateProfile(Admin admin)    //passes the admin data to the UpdateProfile function in the Admin Repository
        {
            adminRepository.UpdateProfile(admin);
        }
    }
}
