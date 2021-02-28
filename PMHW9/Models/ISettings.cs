using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMHW10.Models
{
    public interface ISettings
    {
        public int PrimesFrom { get; set; }
        public int PrimesTo { get; set; }
    }
}
