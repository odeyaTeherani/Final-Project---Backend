using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Business.Exceptions
{
    public class CustomException: Exception
    {
        public CustomException(string message, int httpErrorCode = 400, object data = null) : base(message)
        {
        }

    }
}
