using FindYourPet.Domain.Enums;
using System;

namespace FindYourPet.Domain.Models
{
    public class Sighting : BaseModel
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
        public byte[] Image { get; set; }
        public bool HasBeenFound { get; set; }
        public bool Approved { get; set; }
    }
}
