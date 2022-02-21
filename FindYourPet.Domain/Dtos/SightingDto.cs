using System;

namespace FindYourPet.Domain.Dtos
{
    public class SightingDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string TypeOfPet { get; set; }
        public string Sex { get; set; }
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
