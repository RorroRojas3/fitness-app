using System;
using System.Net;

namespace Rodrigo.Tech.Model.Exceptions
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException() { }

        public StatusCodeException(string message) : base(message)
        {
        }

        public StatusCodeException(string message, Exception inner)
        : base(message, inner) { }

        public StatusCodeException(HttpStatusCode httpStatusCode, string message = null)
        : this(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode  HttpStatusCode {get; set;}
    }
}
