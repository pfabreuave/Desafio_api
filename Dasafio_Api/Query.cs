using Newtonsoft.Json;

namespace Dasafio_Api
{
    public class Query
    {
        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }
    }
}

   
