using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace backend.Controllers
{
    public class CustomException : Exception
    {
        public HttpStatusCode Status { get; set; }
        public object Value { get; set; }

        public CustomException(string message,HttpStatusCode status = HttpStatusCode.BadRequest,object value = null) : base(message)
        {
            Status = status;
            Value = value;
        }

    }
}
