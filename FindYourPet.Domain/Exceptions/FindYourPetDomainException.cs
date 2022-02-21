using FindYourPet.Domain.Enums;
using System;
using System.Net;

namespace FindYourPet.Domain.Exceptions
{
    public class FindYourPetDomainException : Exception
    {
        public ErrorCodeEnum ErrorCode { get; set; }
        public HttpStatusCode? StatusCode { get; set; }

        public FindYourPetDomainException(string message, ErrorCodeEnum errorCode, 
            HttpStatusCode? statusCode = null)
            : base(message)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
        }

        public FindYourPetDomainException(string message, ErrorCodeEnum errorCode, 
            Exception innerException, HttpStatusCode? statusCode = null)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
        }
    }
}
