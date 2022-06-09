using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasafio_Api
{
    internal class Cambios
    {
        
        [JsonProperty("motd")]
        public Motd Motd { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("historical")]
        public bool Historical { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("result")]
        public double Result { get; set; }
    }

}

