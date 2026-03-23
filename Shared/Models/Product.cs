using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [Required, StringLength(50)]
        public string PName { get; set; }
        
        [Required, Range(1, 99999999)]
        public float Price { get; set; }
        
        [Required]
        public int Stock { get; set; }
        
        [Required]
        public string CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
    }
}
