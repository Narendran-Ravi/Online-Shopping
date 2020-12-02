using System.ComponentModel.DataAnnotations;                    //Usage of Key,Required,RegularExpression
using System.ComponentModel.DataAnnotations.Schema;             //Usage of DatabaseGenerated function

namespace OnlineShopping.DomainModel
{
    public class Register
    {
        /// <summary>
        /// This class Register  creates a table in the OnlineShoppingDb database for the User with the mentioned fields
        /// </summary>
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
