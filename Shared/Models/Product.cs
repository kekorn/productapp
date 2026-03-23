using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required, StringLength(50)]
        public string PName { get; set; }
        [Required, Range(1, 99999999)]
        public float Price { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required, ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
