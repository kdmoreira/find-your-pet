using System.Collections.Generic;

namespace FindYourPet.Domain.Responses
{
    public class DefaultResponseData<T> where T : class
    {
        public IList<T> Data { get; set; }
        public int Count { get; set; }
        public int TotalPages { get; set; }
    }
}
