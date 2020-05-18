using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Entity
{
    [BsonIgnoreExtraElements]
    public class Post
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }

        [BsonIgnoreIfDefault]
        public ObjectId OwnerId { get; set; }

        [BsonIgnoreIfNull]
        public string Text { get; set; }

        [BsonIgnoreIfNull]
        public string DateTime { get; set; }

        [BsonIgnoreIfNull]
        public int Likes { get; set; }

        [BsonIgnoreIfNull]
        public List<string> WhoLiked { get; set; }

        [BsonIgnoreIfNull]
        public List<Comment> Comments { get; set; }
    }
}
