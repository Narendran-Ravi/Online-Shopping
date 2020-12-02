using System.ComponentModel.DataAnnotations;       //Usage of Required and Datatype tags


namespace OnlineShopping.ViewModels
{
    public class AdminViewModel
    {
        [Required(ErrorMessage ="Enter the valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
