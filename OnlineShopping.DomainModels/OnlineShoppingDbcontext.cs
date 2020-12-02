using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DomainModels
{
    class OnlineShoppingDbcontext:DbContext
    {
        public OnlineShoppingDbcontext():base("OnlineShoppingDbcontext")
        {

        }
        public DbSet<Register>Registers { get; set; }
    
    
    }
}
