using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMHW10.Models;
using PMHW10.Models.Impl;
using PMHW10.Services;
using PMHW10.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PMHW10.Controllers
{
    [ApiController]
    [Route("/primes")]
    public class PrimesController : ControllerBase
    {
        public PrimesController(
            ISettings settings,
            IPrimesFinderService finder,
            ILogger<PrimesFinderService> logger)
        {
            this.settings = settings;
            this.logger = logger;
            _finder = finder;
        }
        [HttpGet]
        [Route("{num}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<int>> GetStatusOfPrimeNumber(int num)
        {
            logger.LogInformation("Executing method for checking if this number is prime...");
            settings.PrimesFrom = num;
            var checkIsPrime = await _finder.CheckIsPrime();
            if (checkIsPrime == true)
                return Ok();
            else
                return NotFound();
        }
        [HttpGet]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Result>> GetListOfPrimesInSpecificalRange([FromQuery]string from, [FromQuery]string to)
        {
            if (from == null || to == null)
                return BadRequest();
            bool fromIsValid = int.TryParse(from, out int fromParam);
            bool toIsValid = int.TryParse(to, out int toParam);
            if (!fromIsValid || !toIsValid) //here was optional mistake(in 9 hw), but i fixed it 
            {
                return BadRequest();
            }
            else
            {
                settings.PrimesFrom = fromParam;
                settings.PrimesTo = toParam;
                var result = await _finder.FindPrimesInRange();
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                string jsonOutput = JsonSerializer.Serialize(result, options);
                return Ok(jsonOutput);
            }
        }
        private readonly ISettings settings;
        private readonly ILogger<PrimesFinderService> logger;
        private readonly IPrimesFinderService _finder;
    }

}
