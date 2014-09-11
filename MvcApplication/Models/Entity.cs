using MongoDB.Bson.Serialization.Attributes;

namespace MvcApplication.Models
{
    public abstract class Entity
    {
		[BsonId]
        public string Id { get; set; }
    }
}