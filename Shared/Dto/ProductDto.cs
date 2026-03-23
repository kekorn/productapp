using System.ComponentModel.DataAnnotations;
namespace Shared.Dto
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }
    }
}
