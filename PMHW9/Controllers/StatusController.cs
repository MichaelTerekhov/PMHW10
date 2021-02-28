using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PMHW9.Controllers
{

    [ApiController]
    [Route("/")]
    public class StatusController : ControllerBase
    {
        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult<string> GetInfoAboutApi()
        {
            _logger.LogInformation("Status was checked");
            return Ok("Hello World!\n" +
                        "This web service was made by Michael Terekhov!\n" +
                        "You can easily search for prime numbers,\n" +
                        "pass certain parameters, or do a simple number check (Is it prime)");
        }
       
        private readonly ILogger<StatusController> _logger;
    }
}
