using System;
using System.Net;

namespace backend.Controllers
{
    public class CustomException : Exception
    {
        public HttpStatusCode Status { get; }
        public object Value { get; }

        public CustomException(string message,HttpStatusCode status = HttpStatusCode.BadRequest,object value = null) : base(message)
        {
            Status = status;
            Value = value;
        }
    }
}
