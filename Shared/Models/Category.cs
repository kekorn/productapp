using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [Required, StringLength(50)]
        public string CName { get; set; }
        
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
