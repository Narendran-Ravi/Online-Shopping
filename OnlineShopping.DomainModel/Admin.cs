using System.ComponentModel.DataAnnotations;                    //Usage of Key,Required,RegularExpression
using System.ComponentModel.DataAnnotations.Schema;             //Usage of DatabaseGenerated function


namespace OnlineShopping.DomainModel
{
    public class Admin
    {

        /// <summary>
        /// This class Admin  creates a table in the OnlineShoppingDb database with the mentioned fields
        /// </summary>
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminId { get; set; }

        [Required(ErrorMessage="FirstName is Required")]
        [RegularExpression(@"^[A-Za-z ]*$" ,ErrorMessage ="Alphabets Only")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last Name is Required")]
        [RegularExpression(@"^[A-Za-z ]*$" ,ErrorMessage="Alphabets Only")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
