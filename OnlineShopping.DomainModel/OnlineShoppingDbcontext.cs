using System.Data.Entity;      //Usage of Dbcontext base class

namespace OnlineShopping.DomainModel
{
    public class OnlineShoppingDbcontext : DbContext
    {
        public OnlineShoppingDbcontext():base("OnlineShoppingDbcontext")
        {
        }

        public DbSet<Register>Registers { get; set; }
        public DbSet<Admin>Admin { get; set; }

      
        public DbSet<Producttable> Producttables { get; set; }
        public DbSet<BuyRequest> BuyRequests { get; set; }
        public DbSet<CompletedOrders> CompletedOrders { get; set; }
        public DbSet<Cart> Carts { get; set; }
       

    }
}
