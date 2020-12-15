
using System.ComponentModel.DataAnnotations;    //Usage of Required,Display tags



namespace OnlineShopping.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "ID is Required")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = " cate Required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Brand Required")]
        public string Brand { get; set; }

        public int quantity { get; set; }

        [Required(ErrorMessage ="Stock is Required")]
        public int Stock { get; set; }


        [Required(ErrorMessage = "Required ss")]
        public decimal Price { get; set; }

        public string Description { get; set; }

    }
}
