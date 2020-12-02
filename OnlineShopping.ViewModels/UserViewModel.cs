using System.ComponentModel.DataAnnotations;            //Usage of Required and Datatype tags


namespace OnlineShopping.ViewModels
{
    public class UserViewModel
    {
       
     
        [Required(ErrorMessage = "Please enter the Valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the Valid Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
