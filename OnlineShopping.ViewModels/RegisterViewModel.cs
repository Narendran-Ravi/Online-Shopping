using System.ComponentModel.DataAnnotations;        //Usage of Required,Display,Regular Expression tags


namespace OnlineShopping.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Required")]
        public int UserID { get; set; }

        [RegularExpression(@"^[A-Za-z ]*$",ErrorMessage ="Alphabets Only")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is Required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Alphabets Only")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is Required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password should be same")]
        public string ConfirmPassword { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone Number is Required")]
        [Display(Name = "Phone Number:")]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage ="Enter a valid Phone Number(10 digits)")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address:")]
        public string Address { get; set; }


    }
}
