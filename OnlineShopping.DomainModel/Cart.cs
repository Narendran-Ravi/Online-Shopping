using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopping.DomainModel
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int ProductID { get; set; }
        public string Email { get; set; }
        [ForeignKey("ProductID")]
        public virtual Producttable producttable { get; set; }
    }
}
