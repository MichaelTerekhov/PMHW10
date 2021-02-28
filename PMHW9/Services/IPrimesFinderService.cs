using PMHW10.Models.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMHW10.Services
{
    public interface IPrimesFinderService
    {
       public Task<bool> CheckIsPrime(int num);
       public Task<Result> FindPrimesInRange(int from, int to);

    }
}
