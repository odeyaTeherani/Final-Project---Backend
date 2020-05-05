using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            if (error is CustomException ce)
            {
                return Problem(
                    statusCode: (int?)ce.Status,
                    detail: ce.StackTrace,
                    title: ce.Message);
            }

            return Problem();
        }

        [Route("/error")]
        public IActionResult Error()
        {
            //var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            //var error = context.Error;
            return Problem();
        }


        
    } 
}
