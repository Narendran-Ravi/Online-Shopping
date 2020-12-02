using System.Linq;
using OnlineShopping.DomainModel;

namespace OnlineShopping.Repositories
{
    public interface IRegisterRepository
    {
        void Register(Register register);
        void AlreadyExisting(Register register, out bool email, out bool phoneNumber);
    }
    public class RegisterRepository:IRegisterRepository
    {
        OnlineShoppingDbcontext onlineShoppingDbcontext;

        public RegisterRepository()
        {
        onlineShoppingDbcontext= new OnlineShoppingDbcontext();
        }
        public void Register(Register register)
        {
            onlineShoppingDbcontext.Registers.Add(register);
            onlineShoppingDbcontext.SaveChanges();
        }

        public void AlreadyExisting(Register register,out bool email,out bool phoneNumber)
        {
            email = onlineShoppingDbcontext.Registers.Any(m => m.Email == register.Email);
            phoneNumber = onlineShoppingDbcontext.Registers.Any(m => m.PhoneNumber == register.PhoneNumber);
        }
    }
}
