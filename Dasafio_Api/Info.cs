using Newtonsoft.Json;

namespace Dasafio_Api
{
    public class Info
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }
    }
}