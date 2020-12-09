using System.ComponentModel.DataAnnotations;                    //Usage of Key,Required,RegularExpression
using System.ComponentModel.DataAnnotations.Schema;             //Usage of DatabaseGenerated function


namespace OnlineShopping.DomainModel
{
    public class BuyRequest
    {
        /// <summary>
        /// This class BuyRequest  creates a table in the OnlineShoppingDb database with the mentioned fields for the Customer Buy request
        /// </summary>

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId{ get; set; }
        public int ProductId { get; set; }
        public string Email { get; set; }
        public int quantity { get; set; }
        public virtual Producttable producttable { get; set; }
    }
}
