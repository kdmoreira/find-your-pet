using System;

namespace FindYourPet.Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }

        public BaseModel()
        {
            CreateDate = DateTime.Now;
            Active = true;
        }
    }
}
