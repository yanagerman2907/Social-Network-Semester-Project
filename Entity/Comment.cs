using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entity
{
    public class Comment
    {
        [BsonIgnoreIfDefault]
        public ObjectId OwnerId { get; set; }

        [BsonIgnoreIfNull]
        public string Text { get; set; }

        [BsonIgnoreIfNull]
        public string DateTime { get; set; }
    }
}