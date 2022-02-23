using FindYourPet.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FindYourPet.Infra.Data.Entities
{
    public class SightingEntity : BaseEntity
    {
        public DateTime Date { get; set; }
        public TypeOfPetEnum TypeOfPet { get; set; }
        public SexEnum Sex { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public string Complement { get; set; }
        public string ImageUrl { get; set; }

        [BsonRepresentation(BsonType.Binary)]
        public byte[] Image { get; set; }
        public bool HasBeenFound { get; set; }
        public bool Approved { get; set; }
    }
}