using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DomainModel
{
    public class CompletedOrders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompletedOrderId { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public string Email { get; set; }
        public int quantity { get; set; }

        public DateTime DateTime { get; set; }
        [ForeignKey("ProductId")]
        public virtual Producttable producttable { get; set; }
    }
}
