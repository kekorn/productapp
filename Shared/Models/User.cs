using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [Required, StringLength(50)]
        public string Username { get; set; }
        
        [Required, StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = "User";
    }
}
