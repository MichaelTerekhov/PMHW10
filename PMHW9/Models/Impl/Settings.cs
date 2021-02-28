using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PMHW10.Models.Impl
{
    public class Settings : ISettings
    {
        [JsonPropertyName("primesFrom")]
        public int PrimesFrom { get; set; }
        [JsonPropertyName("primesTo")]
        public int PrimesTo { get; set; }
    }
}
