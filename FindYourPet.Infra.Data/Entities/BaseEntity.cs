using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FindYourPet.Infra.Data.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
    }
}
