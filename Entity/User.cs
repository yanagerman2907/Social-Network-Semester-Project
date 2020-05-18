using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Entity
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfNull]
        [JsonProperty(PropertyName = "name")]
        public string Login { get; set; }

        [BsonIgnoreIfNull]
        public string FirstName { get; set; }

        [BsonIgnoreIfNull]
        public string LastName { get; set; }

        [BsonIgnoreIfNull]
        public string HashedPassword { get; set; }

        [BsonIgnoreIfNull]
        public string LastLogin { get; set; }
    }
}
