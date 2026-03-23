using System.ComponentModel.DataAnnotations;
namespace Shared.Dto
{
    public class ProductDto
    {
        public string? Id { get; set; }
        public int ProductID { get; set; }
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "Please select a category.")]
        public string? CategoryObjectId { get; set; }
    }
}
