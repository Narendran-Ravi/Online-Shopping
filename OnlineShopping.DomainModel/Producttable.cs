using System.ComponentModel.DataAnnotations;                    //Usage of Key,Required,RegularExpression
using System.ComponentModel.DataAnnotations.Schema;             //Usage of DatabaseGenerated function


namespace OnlineShopping.DomainModel
{
    public class Producttable
    {
        /// <summary>
        /// This class Producttable  creates a table in the OnlineShoppingDb database  with the mentioned fields for the products
        /// </summary>
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
