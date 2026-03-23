using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required, StringLength(50)]
        public string CName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
