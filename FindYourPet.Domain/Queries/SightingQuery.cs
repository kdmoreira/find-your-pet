using FindYourPet.Domain.Enums;

namespace FindYourPet.Domain.Queries
{
    public class SightingQuery
    {
        public string Municipality { get; set; }
        public string ZipCode { get; set; }
        public TypeOfPetEnum TypeOfPet { get; set; }
    }
}
