using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OnlineShopping.DomainModel;
using OnlineShopping.ViewModels;

namespace OnlineShopping.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> AdminLogin(AdminViewModel adminViewModel);
        Admin GetEmail(string email);
        void UpdateProfile(Admin admin);
        IEnumerable<CompletedOrders> CompletedOrders();
    }
    public class AdminRepository:IAdminRepository
    {
        OnlineShoppingDbcontext onlineShoppingDbcontext;

        public AdminRepository()
        {
            onlineShoppingDbcontext = new OnlineShoppingDbcontext();
        }
        public IEnumerable<Admin> AdminLogin(AdminViewModel adminLoginViewModel)
        {
            return onlineShoppingDbcontext.Admin.Where(m => m.Email == adminLoginViewModel.Email && m.Password == adminLoginViewModel.Password).ToList();
        }

        public Admin GetEmail(string email)
        {
            var data = onlineShoppingDbcontext.Admin.Where(x => x.Email == email).SingleOrDefault();
            return data;
          
         }

        public void UpdateProfile(Admin admin)
        {
            onlineShoppingDbcontext.Entry(admin).State = EntityState.Modified;
            onlineShoppingDbcontext.SaveChanges();
        }

        public IEnumerable<CompletedOrders> CompletedOrders()
        {
            IEnumerable<CompletedOrders> completedOrders = onlineShoppingDbcontext.CompletedOrders.ToList();
            return completedOrders;
        }
    }
}
