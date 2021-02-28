using Microsoft.Extensions.Logging;
using PMHW10.Models;
using PMHW10.Models.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PMHW10.Services.Impl
{
    public class PrimesFinderService:IPrimesFinderService
    {
        public PrimesFinderService(ILogger<PrimesFinderService> logger)
        {
            this.logger = logger;
        }
        public Task<bool> CheckIsPrime(int num)
        {
            logger.LogInformation("Trying to get settings to check is it number is prime.");
            if (num < 2)
            {
                logger.LogWarning($"Can`t to check, because settings param: {num}\n" +
                    $"does not meet the possible conditions for searching prime numbers");
                return Task.FromResult(false);
            }
            else
            {
                var result = PrimeAlgoFinder(num);
                logger.LogInformation($"Operation succeded: Number -> {num} Prime: {result}");
                return Task.FromResult(result);
            }
        }
        public async Task<Result> FindPrimesInRange(int from, int to)
        {
            return await Task.Run(() =>
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                logger.LogInformation("Trying to get settings to check is it number is prime.");
                List<int> result = new List<int>();
                if ((from > to) && from > 0)
                {
                    timer.Stop();

                    logger.LogInformation($"Primes in range from {from} to {to} wasn`t found");
                    return new Result
                    {
                        Duration = TimeParser(timer),
                        Error = null,
                        Primes = result,
                        Success = true
                    };
                }
                if (from < 0 && to < 0)
                {
                    timer.Stop();
                    logger.LogInformation($"Primes in range from {from} to {to} wasn`t found");
                    return new Result
                    {
                        Duration = TimeParser(timer),
                        Error = null,
                        Primes = result,
                        Success = true
                    };
                }

                for (var i = from; i < to + 1; i++)
                {
                    if (i <= 1) continue;
                    var isPrime = true;
                    for (var j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (!isPrime) continue;
                    result.Add(i);
                }
                timer.Stop();
                logger.LogInformation($"Primes in range from {from} to {to} was found succesfully");
                return new Result
                {
                    Duration = TimeParser(timer),
                    Error = null,
                    Primes = result,
                    Success = true
                };
            });
        }
        private bool PrimeAlgoFinder(int num)
        {
            for (int i = 2; i < num; i++)
                if (num % i == 0)
                    return false;
            return true;
        }

        private static string TimeParser(Stopwatch time)
        {
            TimeSpan ts = time.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
            return elapsedTime;
        }
        private ILogger<PrimesFinderService> logger;
    }
}
