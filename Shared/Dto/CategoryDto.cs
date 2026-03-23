using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }
        [Required, StringLength(50), Display(Name = "Name")]
        public string CName { get; set; }
    }
}
