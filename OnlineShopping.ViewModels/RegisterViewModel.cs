using System.ComponentModel.DataAnnotations;        //Usage of Required,Display,Regular Expression tags
using System.Web.Security;

namespace OnlineShopping.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Required")]
        public int UserID { get; set; }

        [RegularExpression(@"^[A-Za-z ]*$",ErrorMessage ="Alphabets Only")]
        [Required(ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is Required")]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Alphabets Only")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required( ErrorMessage = "Email address is Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required( ErrorMessage = "Password is Required")]
        //        [MembershipPassword(
        //    MinRequiredNonAlphanumericCharacters = 1,
        //    MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
        //    ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
        //    MinRequiredPasswordLength = 6,
        //            MaxRequriedPasswordLength=7
        //)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])(?=.*[\S]).{6,15}$", ErrorMessage = "Password must be between 6 and 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password should be same")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage ="Enter a valid Phone Number(10 digits)")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }


    }
}
