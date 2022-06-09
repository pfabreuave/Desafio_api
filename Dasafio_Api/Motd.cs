using Newtonsoft.Json;
using System;

namespace Dasafio_Api
{
    public class Motd
    {

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}